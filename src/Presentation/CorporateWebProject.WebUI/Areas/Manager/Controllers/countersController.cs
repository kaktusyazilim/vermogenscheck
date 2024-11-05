using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class countersController : BaseController<Users>
    {
        private readonly ICounterRepository _counterRepository;
        private readonly IMemoryCache _memoryCache;
        public countersController(ICounterRepository counterRepository, IMemoryCache memoryCache) : base(counterRepository, AuthPage.Counters)
        {
            _counterRepository = counterRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.Counters)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.CounterList = (await _counterRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.Counters)]
        public IActionResult Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Counters)]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            if (fc.Files["files"]!=null)
            {
                var imageResult = base.CreateFile(fc.Files["files"]);
                model.Counter.Icon = imageResult.Path;
                var result = await _counterRepository.AddAsync(model.Counter);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/counters");
            }
            base.SetResponseMessage(false);
            return Redirect("/manager/counters");
        }

        [HttpGet]
        [Auth("Update", AuthPage.Counters)]
        public IActionResult Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.Counter = _counterRepository.Get(x => x.ItemGuid == id).Result.Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Counters)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = _counterRepository.Get(x => x.ItemGuid == model.Counter.ItemGuid).Result.Data;
            if(currentItem!=null)
            {
                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    currentItem.Icon = imageResult.Path;
                    
                }
                currentItem.Queue = model.Counter.Queue;
                currentItem.Name = model.Counter.Name;
                currentItem.Value=model.Counter.Value;
                currentItem.LangId=model.Counter.LangId;
                var result = await _counterRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/counters");
            }
            return View(model);
        }
    }
}


