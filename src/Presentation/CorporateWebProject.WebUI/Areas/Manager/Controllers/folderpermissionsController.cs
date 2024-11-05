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
    public class folderpermissionsController : BaseController<Folders>
    {
        private readonly IFolderPermissionRepository _folderPermissionRepository;
        private readonly IFolderRepository _folderRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMemoryCache _memoryCache;
        public folderpermissionsController(IFolderPermissionRepository folderPermissionRepository, IFolderRepository folderRepository, IDocumentRepository documentRepository, ICompanyRepository companyRepository, IMemoryCache memoryCache) : base(folderRepository, AuthPage.FolderPermissions)
        {
            _folderPermissionRepository = folderPermissionRepository;
            _folderRepository = folderRepository;
            _documentRepository = documentRepository;
            _companyRepository = companyRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.FolderPermissions)]
        public IActionResult Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.FolderPermissions)]
        public IActionResult Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.FolderPermissions)]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            string folderName = FriendlyUrl.FriendlyURLTitle(model.Folder.Name);

            // Klasörün oluşturulacağı yol
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", folderName);

            // Klasör yoksa oluşturulur
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            model.Folder.FilePath = "/uploads/"+folderName+"/";
            var result =await _folderRepository.AddAsync(model.Folder);
            if(model.FolderPermissionList!=null)
            {
                foreach (var item in model.FolderPermissionList)
                {
                    item.FolderGuid = result.Data.ItemGuid;
                    await _folderPermissionRepository.AddAsync(item);
                }
            }
         if(model.Folder.TopGuid == "")
            return Redirect("/manager/documents");
            else
                return Redirect("/manager/documents/document?id=" + model.Folder.TopGuid);
            return View(model);
        }

        [HttpGet]
        [Auth("Update", AuthPage.FolderPermissions)]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.DocumentList =(await _documentRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.FolderList = (await _folderRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.Folder=(await _folderRepository.Get(x => x.ItemGuid == id)).Data;
            model.CompanyList = (await _companyRepository.GetListAsync()).Data;
            model.CurrentFolderPermissionList = (await _folderPermissionRepository.GetListAsync(x => x.FolderGuid == model.Folder.ItemGuid)).Data;
            model.FolderPermissionList = new List<FolderPermissions>();
            model.CompanyList.ForEach(x =>
            {
                model.FolderPermissionList.Add(new FolderPermissions()
                {
                    FolderGuid=id,
                    CompanyGuid = x.ItemGuid,

                });
            });
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.FolderPermissions)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
               await model.FillDataAsync(HttpContext);
            var currentItem = _folderRepository.Get(x => x.ItemGuid == model.Folder.ItemGuid).Result.Data;
            if (currentItem != null)
            {
                currentItem.Name = model.Folder.Name;
                currentItem.CompanyGuid = model.Folder.CompanyGuid;
                currentItem.TopGuid = model.Folder.TopGuid;
                currentItem.FilePath = model.Folder.FilePath;
                currentItem.IsDeleted = model.Folder.IsDeleted;
                currentItem.IsGeneral = model.Folder.IsGeneral;
                currentItem.UpdateUserId = model.CurrentUser != null ? model.CurrentUser.Id : model.CurrentCompany.Id;
                var result = await _folderRepository.UpdateAsync(currentItem);
                if (model.FolderPermissionList != null)
                {
                    foreach (var item in model.FolderPermissionList)
                    {
                        item.FolderGuid = currentItem.ItemGuid;
                        await _folderPermissionRepository.AddAsync(item);
                    }
                }
                return Redirect("/manager/documents");
            }
            return View(model);
        }
    }
}