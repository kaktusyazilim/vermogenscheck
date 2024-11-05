using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("manager")]
    public class filesController : BaseController<Files>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IFolderRepository _folderRepository;
        private readonly IMemoryCache _memoryCache;
        public filesController(IFileRepository fileRepository, IDocumentRepository documentRepository, IFolderRepository folderRepository, IMemoryCache memoryCache) : base(fileRepository, AuthPage.Files)
        {
            _fileRepository = fileRepository;
            _documentRepository = documentRepository;
            _folderRepository = folderRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.Files)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Auth("Create", AuthPage.Files)]
        public IActionResult Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Files)]
        public IActionResult Create(ServiceVM model, IFormCollection fc)
        {
            if (fc != null)
            {
                var releateid = fc["releatedid"];
                var typeid = fc["Type.Id"];

                var imageResult = CreateFile(fc.Files[0]);
                var result = _fileRepository.AddAsync(new Files()
                {
                    CreateDate = DateTime.Now,
                    FilePath = imageResult.Path,
                    ReleatedId = releateid.ToString(),
                    TypeId = Convert.ToInt32(typeid)

                });
                return new JsonResult(new
                {
                    uploaded = 1,
                    fileName = imageResult.FileName,
                    url = imageResult.Path
                });
                return Json("Y");
            }

            return View(model);
        }


        [HttpPost]
        [Auth("Create", AuthPage.Files)]
        public async Task<IActionResult> UploadData(ServiceVM model, IFormCollection fc)
        {
               await model.FillDataAsync(HttpContext);
            var releateid = fc["releatedid"];
            var folderGuid = fc["folderGuid"].ToString();
            var companyGuid = fc["companyGuid"].ToString();
            var typeid = fc["Type.Id"];
            if (folderGuid != "")
            {
                var currentFolder = _folderRepository.Get(x => x.ItemGuid == folderGuid).Result.Data;
                var imageResult = CreateFile(fc.Files[0], currentFolder.FilePath);
                var result = await _documentRepository.AddAsync(new Documents()
                {
                    Type = imageResult.CurrentFile.ContentType,
                    FilePath = imageResult.Path,
                    FolderGuid = folderGuid,
                    Name = imageResult.CurrentFile.FileName,
                    Size = imageResult.CurrentFile.Length,
                    CreateUserId = model.CurrentUser != null ? model.CurrentUser.Id : model.CurrentCompany.Id,
                    UpdateUserId = model.CurrentUser != null ? model.CurrentUser.Id : model.CurrentCompany.Id,

                });
                return new JsonResult(result);
            }
            return Json("Y");
            return View(model);
        }

        [HttpGet]
        [Auth("Update", AuthPage.Files)]
        public IActionResult Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Files)]
        public IActionResult Update(ServiceVM model, IFormCollection fc)
        {
            return View(model);
        }
        [HttpGet]
        [Auth("Read", AuthPage.Files)]
        public IActionResult Browse(IFormCollection fc)
        {
            var services = HttpContext.RequestServices;
            var _hostEnvironment = (IWebHostEnvironment)services.GetService(typeof(IWebHostEnvironment));
            var rootPath = _hostEnvironment.WebRootPath;
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), _hostEnvironment.WebRootPath, "uploads"));
            model.LocalFiles = dir.GetFiles();
            return View(model);
        }

    }
}
