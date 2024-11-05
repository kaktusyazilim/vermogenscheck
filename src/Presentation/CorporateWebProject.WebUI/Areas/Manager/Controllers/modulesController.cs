using CorporateWebProject.Application.Parameters;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.FriendlyUrl;
using CorporateWebProject.Domain.Common;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.Persistence.Repositories;
using CorporateWebProject.WebUI.Handlers.Authorization;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Protocol.Core.Types;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("manager")]
    public class ModulesController : BaseController<Modules>
    {
        private readonly IModulRepository _ModulRepository;
        private readonly ILanguageRepository _LanguageRepository;
        private readonly IPageRepository _PageRepository;
        private readonly IMemoryCache _memoryCache;
        public ModulesController(IModulRepository modulRepository, ILanguageRepository languageRepository, IPageRepository pageRepository, IMemoryCache memoryCache) : base(modulRepository, AuthPage.Modules)
        {

            _ModulRepository = modulRepository;
            _LanguageRepository = languageRepository;
            _PageRepository = pageRepository;
            _memoryCache = memoryCache;
        }

        [Auth(AuthTypes.Read, AuthPage.Modules)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.NewModul = (await _ModulRepository.Get(x => x.Url.ToUpper() == "MODULES")).Data;
            model.NewModulList = _ModulRepository.GetListAsync(x => x.IsDeleted == false).Result.Data;
            return View(model);
        }

        [Auth(AuthTypes.Create, AuthPage.Modules)]
        [HttpGet]
        public async Task<IActionResult> create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.LanguageList = (await _LanguageRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.NewModulList = _ModulRepository.GetListAsync(x => x.IsDeleted == false).Result.Data;

            return View(model);
        }

        [Auth(AuthTypes.Create, AuthPage.Modules)]
        [HttpPost]
        public async Task<IActionResult> create(ServiceVM model)
        {
            model.NewModul.ItemGuid = model.NewModul.Title.ToUpper().Replace(" ", "");
            var result = await _ModulRepository.AddAsync(model.NewModul);

            if (result.Success == true)
            {
                var isPage = (await _PageRepository.GetListAsync(x => x.ModulId == result.Data.Id)).Data;
                if (!(isPage != null && isPage.Count() != 0))
                {
                    var pageResult = await _PageRepository.AddAsync(new Pages()
                    {
                        Descriptions = "",
                        IsShow = true,
                        LangId = result.Data.LangId,
                        PageGuid = FriendlyUrl.FriendlyURLTitle(model.NewModul.Title).Length > 20 ? FriendlyUrl.FriendlyURLTitle(model.NewModul.Title).Substring(0, 20) : FriendlyUrl.FriendlyURLTitle(model.NewModul.Title),
                        ModulId = result.Data.Id,
                        Title = "Liste",
                        Url = "index",
                        Icon = "",


                    });
                    base.SetResponseMessage(result.Success);
                    return Redirect("/manager/modules");

                }
            }
            base.SetResponseMessage(false);

            return Redirect("/manager/modules");
        }

        [Auth(AuthTypes.Update, AuthPage.Modules)]
        [HttpGet]
        public async Task<IActionResult> update(int id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.LanguageList = (await _LanguageRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.NewModul = (await _ModulRepository.Get(x => x.Id == id)).Data;
            model.NewModulList = _ModulRepository.GetListAsync(x => x.IsDeleted == false).Result.Data;
            return View(model);
        }

        [Auth(AuthTypes.Update, AuthPage.Modules)]
        [HttpPost]
        public async Task<IActionResult> update(ServiceVM model)
        {
            var currentItem = _ModulRepository.Get(x => x.Id == model.NewModul.Id).Result.Data;
            var queues = (await _ModulRepository.GetListAsync(x => x.Id != currentItem.Id && x.IsPassive == false && x.IsDeleted == false & x.Queue >= model.NewModul.Queue)).Data.OrderBy(x => x.Queue).ToList();
            int arttir = 1;
            for (int i = 0; i < queues.Count(); i++)
            {
                queues[i].Queue = model.NewModul.Queue + arttir;
                var updateResult = await _ModulRepository.UpdateAsync(queues[i]);

                arttir++;
            }
            currentItem.Title = model.NewModul.Title;
            currentItem.Queue = model.NewModul.Queue;
            currentItem.ModifiedDate = DateTime.Now;
            currentItem.LangId = model.NewModul.LangId;
            currentItem.Url = model.NewModul.Url;
            currentItem.IsShow = model.NewModul.IsShow;
            var result = await _ModulRepository.UpdateAsync(currentItem);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/modules");
        }

    }
}
