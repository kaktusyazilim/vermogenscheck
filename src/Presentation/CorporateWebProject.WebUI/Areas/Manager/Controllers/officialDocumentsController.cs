using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class OfficialDocumentsController : BaseController<OfficialDocuments>
    {
        private readonly IOfficialDocumentRepository _documentRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMemoryCache _memoryCache;
        public OfficialDocumentsController(IOfficialDocumentRepository documentRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IMemoryCache memoryCache) : base(documentRepository, AuthPage.OfficialDocuments)
        {
            _documentRepository = documentRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.OfficialDocuments)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.OfficialDocumentList = (await _documentRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.OfficialDocuments)]
        public async Task<IActionResult> Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.TypeId == model.Type.Id)).Data;
            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.TypeId == model.Type.Id)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.OfficialDocuments)]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            if (fc.Files["files"]!=null)
            {
                var fileResult = base.CreateFile(fc.Files["files"]);
                model.OfficialDocument.FilePath = fileResult.Path;
                var result = await _documentRepository.AddAsync(model.OfficialDocument);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/OfficialDocuments");
            }
            return View(model);
        }

        [HttpGet]
        [Auth("Update", AuthPage.OfficialDocuments)]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.OfficialDocument = (await _documentRepository.Get(x => x.ItemGuid == id)).Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.TypeId == model.Type.Id)).Data;
            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.TypeId == model.Type.Id)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.OfficialDocuments)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem=(await _documentRepository.Get(x => x.ItemGuid == model.OfficialDocument.ItemGuid)).Data;
            if(currentItem!=null)
            {
                currentItem.SubCategoryGuid = model.OfficialDocument.SubCategoryGuid;
                currentItem.CategoryGuid=model.OfficialDocument.CategoryGuid;
                currentItem.Name= model.OfficialDocument.Name;
                currentItem.IsSubTitle=model.OfficialDocument.IsSubTitle;
                currentItem.Color = model.OfficialDocument.Color;
                if (fc.Files["files"] != null)
                {
                    var fileResult = base.CreateFile(fc.Files["files"]);
                    currentItem.FilePath = fileResult.Path;
                 
                }
                var result = await _documentRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/OfficialDocuments");
            }
            return View(model);
        }
    }
}


