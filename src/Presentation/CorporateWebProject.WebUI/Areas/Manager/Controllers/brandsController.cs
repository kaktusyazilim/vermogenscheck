using CorporateWebProject.Application.Repositories.Brands;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class brandsController : BaseController<Brands>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMemoryCache _memoryCache;

        public brandsController(IBrandRepository brandRepository, IMemoryCache memoryCache) : base(brandRepository, AuthPage.Brands)
        {
            _brandRepository = brandRepository;
            _memoryCache = memoryCache;
        }


        [Auth("Read", AuthPage.Brands)]
        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.BrandList = (await _brandRepository.GetListAsync()).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.Brands)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }

        [Auth("Create", AuthPage.Brands)]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            if (fc != null)
            {
                var imageResult = base.CreateFile(fc.Files["files"]);
                model.Brand.FilePath = imageResult.Path;
                model.Brand.Name = model.Brand.Name ?? "";
                var result = await _brandRepository.AddAsync(model.Brand);
                base.SetResponseMessage(result.Success);
            }
            return Redirect("/manager/brands");

        }

        [Auth("Update", AuthPage.Brands)]
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Brand = (await _brandRepository.Get(x => x.ItemGuid == id)).Data;
            return View(model);
        }

        [Auth("Update", AuthPage.Brands)]
        [HttpPost]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = (await _brandRepository.Get(x => x.ItemGuid == model.Brand.ItemGuid)).Data;
            if (currentItem != null)
            {
                if (fc != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    model.Brand.FilePath = imageResult.Path;
                }
                model.Brand.Name = model.Brand.Name ?? "";
                base.Equalize(currentItem, model.Brand);
                var result = await _brandRepository.UpdateAsync(model.Brand);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/brands");
            }

            return View(model);

        }


    }
}
