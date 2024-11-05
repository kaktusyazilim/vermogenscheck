using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class languagesController : BaseController<SmtpSettings>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMemoryCache _memoryCache;

        public languagesController(ILanguageRepository languageRepository, IMemoryCache memoryCache) : base(languageRepository, AuthPage.Languages)
        {
            _languageRepository = languageRepository;
            _memoryCache = memoryCache;
        }

        [Auth("Read", AuthPage.Languages)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.LanguageList = (await _languageRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.Languages)]
        [HttpGet]
        public IActionResult Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }
        [Auth("Create", AuthPage.Languages)]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            if (fc.Files["flag"]!=null)
            {
                var imageResult = base.CreateFile(fc.Files["flag"]);
                model.Language.Image = imageResult.Path;
                await _languageRepository.AddAsync(model.Language);
                return Redirect("/manager/languages");
            }
            return View();
        }


        [Auth("Update", AuthPage.Languages)]
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.Language = (await _languageRepository.Get(x => x.ItemGuid == id)).Data;
            model.LanguageList = (await _languageRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }
        [Auth("Update", AuthPage.Languages)]
        [HttpPost]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem=_languageRepository.Get(x=>x.ItemGuid==model.Language.ItemGuid).Result.Data;    
            if(currentItem!=null)
            {
                if (fc.Files["flag"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["flag"]);
                    currentItem.Image = imageResult.Path;
                    
                }
                currentItem.Name=model.Language.Name;
                currentItem.CultureCode = model.Language.CultureCode;
                currentItem.Code=   model.Language.Code;
                await _languageRepository.UpdateAsync(currentItem);
                return Redirect("/manager/languages");
            }
            return View();

        }
    }
}
