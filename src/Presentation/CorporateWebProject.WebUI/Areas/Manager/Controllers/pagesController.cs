using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class pagesController : BaseController<Pages>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IModulRepository _modulRepository;
        private readonly IMemoryCache _memoryCache;
        public pagesController(IUserRepository userRepository, IPageRepository pageRepository, IModulRepository modulRepository, IMemoryCache memoryCache) : base(pageRepository, AuthPage.Pages)
        {
            _userRepository = userRepository;
            _pageRepository = pageRepository;
            _modulRepository = modulRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.Pages)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.PageList = (await _pageRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Create",AuthPage.Pages)]
        public async Task<IActionResult> Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.ModulList=(await _modulRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.Page = new Pages();
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Pages)]
        public async Task<IActionResult> Create(ServiceVM model,IFormCollection fc)
        {
            var result =await _pageRepository.AddAsync(model.Page);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/pages");
        }

        [HttpGet]
        [Auth("Update", AuthPage.Pages)]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.Page = (await _pageRepository.Get(x => x.ItemGuid == id)).Data;
            model.ModulList = (await _modulRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;

            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Pages)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem=(await _pageRepository.Get(x => x.ItemGuid == model.Page.ItemGuid)).Data;
            if(currentItem!=null)
            {
               currentItem.Title=model.Page.Title;
                currentItem.Queue=model.Page.Queue;
                currentItem.Descriptions=model.Page.Descriptions;   
                currentItem.IsShow=model.Page.IsShow;
                currentItem.LangId=model.Page.LangId;
                currentItem.ModulId =model.Page.ModulId;
                currentItem.Url=model.Page.Url;
                currentItem.PageGuid = model.Page.PageGuid;
                var result= _pageRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Result.Success);
                return Redirect("/manager/pages");
            }
            base.SetResponseMessage(false);
            return Redirect("/manager/pages");

        }
    }
}
