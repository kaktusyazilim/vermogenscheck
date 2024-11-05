using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class galleriesController : BaseController<Gallery>
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMemoryCache _memoryCache;
        public galleriesController(IGalleryRepository galleryRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IMemoryCache memoryCache) : base(galleryRepository, AuthPage.Users)
        {
            _galleryRepository = galleryRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.Users)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.GalleryList = (await _galleryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.TypeId == model.Type.Id)).Data;

            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.Users)]
        public async Task<IActionResult> Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.TypeId == model.Type.Id)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Users)]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            model.Gallery.Name = model.Gallery.Name == null ? "" : model.Gallery.Name;
            if (fc.Files["files"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["files"]);
                model.Gallery.FilePath = imageResult.Path;
                var result = await _galleryRepository.AddAsync(model.Gallery);

                base.SetResponseMessage(result.Success);
                return Redirect("/manager/galleries");

            }
            return View(model);
        }

        [HttpGet]
        [Auth("Update", AuthPage.Users)]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.Gallery = _galleryRepository.Get(x => x.ItemGuid == id).Result.Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.TypeId == model.Type.Id)).Data;

            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Users)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = _galleryRepository.Get(x => x.ItemGuid == model.Gallery.ItemGuid).Result.Data;
            if (currentItem != null)
            {
                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    currentItem.FilePath = imageResult.Path;
                }
                currentItem.CategoryId = model.Gallery.CategoryId;
                currentItem.Name = model.Gallery.Name;
                currentItem.Queue = model.Gallery.Queue;
                var result = await _galleryRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/galleries");

            }
            return View(model);
        }
    }
}