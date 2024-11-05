using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.FriendlyUrl;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("manager")]
    public class documentsController : BaseController<Documents>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IFolderRepository _folderRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IUserRepository _userRepository;

        public documentsController(IDocumentRepository documentRepository, ICompanyRepository companyRepository, IFolderRepository folderRepository, IMemoryCache memoryCache, IUserRepository userRepository) : base(documentRepository, AuthPage.Documents)
        {
            _documentRepository = documentRepository;
            _companyRepository = companyRepository;
            _folderRepository = folderRepository;
            _memoryCache = memoryCache;
            _userRepository = userRepository;
        }
        [HttpGet]
        [Auth("Read", AuthPage.Documents)]
        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.DocumentList = (await _documentRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.FolderList = (await _folderRepository.GetListAsync(x => x.IsDeleted == false && x.TopGuid == "")).Data;
            model.CompanyList = (await _companyRepository.GetListAsync()).Data;
            model.FolderPermissionList = new List<FolderPermissions>();
            model.UserList = (await _userRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;

            model.CompanyList.ForEach(x =>
            {
                model.FolderPermissionList.Add(new FolderPermissions()
                {
                    CompanyGuid = x.ItemGuid,

                });
            });
            return View(model);
        }
        [HttpGet]
        [Auth("Read", AuthPage.Documents)]
        public async Task<IActionResult> document(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Folder = _folderRepository.Get(x => x.ItemGuid == id).Result.Data;
            model.DocumentList = (await _documentRepository.GetListAsync(x => x.FolderGuid == model.Folder.ItemGuid && x.IsDeleted == false)).Data;
            model.Company = (await _companyRepository.Get(x => x.ItemGuid == model.Folder.CompanyGuid)).Data;
            model.FolderList = (await _folderRepository.GetListAsync(x => x.TopGuid == id && x.IsDeleted == false)).Data;
            model.SubFolderList = (await _folderRepository.GetListAsync(x => x.TopGuid == model.Folder.ItemGuid && x.IsDeleted == false)).Data;
            model.SubFolder = (await _folderRepository.Get(x => x.ItemGuid == model.Folder.TopGuid)).Data;
            model.UserList = (await _userRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.Documents)]
        public IActionResult Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Documents)]
        public IActionResult Create(ServiceVM model, IFormCollection fc)
        {
            return View(model);
        }
        [HttpPost]
        [Auth("Create", AuthPage.Documents)]
        public IActionResult CreateFolder(ServiceVM model, IFormCollection fc)
        {
            string folderName = FriendlyUrl.FriendlyURLTitle(fc["FileName"]);

            // Klasörün oluşturulacağı yol
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", folderName);

            // Klasör yoksa oluşturulur
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Redirect("/manager/documents");
        }
        [HttpGet]
        [Auth("Update", AuthPage.Documents)]
        public IActionResult Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Documents)]
        public IActionResult Update(ServiceVM model, IFormCollection fc)
        {
            return View(model);
        }
    }
}