using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("manager")]
    public class historyController : BaseController<History>
    {
        private IHistoryRepository _historyRepository;
        private readonly IMemoryCache _memoryCache;
        public historyController(IHistoryRepository historyRepository, IMemoryCache memoryCache) : base(historyRepository, AuthPage.History)
        {
            this._historyRepository = historyRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.History)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.HistoryList = (await _historyRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.History)]
        public IActionResult Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.History)]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            var result =await _historyRepository.AddAsync(model.History);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/history");
        }

        [HttpGet]
        [Auth("Update", AuthPage.History)]
        public IActionResult Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.History=_historyRepository.Get(x=>x.ItemGuid==id).Result.Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.History)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = _historyRepository.Get(x => x.ItemGuid == model.History.ItemGuid).Result.Data;
            if(currentItem!=null)
            {
                currentItem.Queue = model.History.Queue;
                currentItem.Title = model.History.Title;
                currentItem.Description=model.History.Description;
                currentItem.Year= model.History.Year;
                var result=await _historyRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/history");
            }
            return View(model);
        }
    }
}


