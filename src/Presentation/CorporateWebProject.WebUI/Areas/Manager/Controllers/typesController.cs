using CorporateWebProject.Application.Repositories;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class typesController : BaseController<CorporateWebProject.Domain.Entities.Types>
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IModulRepository _ModulRepository;
        private readonly IMemoryCache _memoryCache;
        public typesController(ITypeRepository typeRepository, IModulRepository modulRepository, IMemoryCache memoryCache) : base(typeRepository, AuthPage.Type)
        {
            _typeRepository = typeRepository;
            _ModulRepository = modulRepository;
            _memoryCache = memoryCache;
        }
        [Auth("Read", AuthPage.Type)]
        public async Task<IActionResult> Index()
        {
            ServiceVM serviceVM = new ServiceVM(HttpContext, _memoryCache);
            serviceVM.TypeList = (await _typeRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            serviceVM.ModulList = (await _ModulRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;

            return View(serviceVM);
        }
        [Auth("Create", AuthPage.Type)]
        [HttpGet]
        public async Task<IActionResult> create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.ModulList = (await _ModulRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.Type)]
        [HttpPost]
        public async Task<IActionResult> create(ServiceVM model)
        {
            model.Type.IsDeleted = false;
            model.Type.IsPassive = false;
            model.Type.CreateDate = DateTime.Now;
            model.Type.ModifiedDate = DateTime.Now;
            var result = await _typeRepository.AddAsync(model.Type);
            base.SetResponseMessage(result.Success);

            return Redirect("/manager/types");
        }

        [Auth("Update", AuthPage.Type)]
        [HttpGet]
        public async Task<IActionResult> update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Type = (await _typeRepository.Get(x => x.ItemGuid == id)).Data;
            model.ModulList = (await _ModulRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;

            return View(model);
        }

        [Auth("Update", AuthPage.Type)]
        [HttpPost]
        public async Task<IActionResult> update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = await _typeRepository.Get(x => x.ItemGuid == model.Type.ItemGuid);
            if (currentItem.Data != null)
            {
                if (fc.Files["pictures"] != null)
                {
                    var image = base.CreateFile(fc.Files["pictures"]);
                    currentItem.Data.Icon = image.Path;
                }
                currentItem.Data.ShortDescription = model.Type.ShortDescription;
                currentItem.Data.IsPage = model.Type.IsPage;
                currentItem.Data.Name = model.Type.Name;
                currentItem.Data.ModulId = model.Type.ModulId;
                currentItem.Data.ShortDescription = currentItem.Data.ShortDescription ?? "";
                await _typeRepository.UpdateAsync(currentItem.Data);
                base.SetResponseMessage(currentItem.Success);
                return Redirect("/manager/types");
            }
            return View();
        }
    }
}
