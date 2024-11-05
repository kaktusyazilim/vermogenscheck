using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.FriendlyUrl;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class eventsController : BaseController<Events>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMemoryCache _memoryCache;

        public eventsController(IEventRepository eventRepository, IMemoryCache memoryCache) : base(eventRepository, AuthPage.SmtpSettings)
        {
            _eventRepository = eventRepository;
            _memoryCache = memoryCache;
        }

        [Auth("Read", AuthPage.SmtpSettings)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.EventList = (await _eventRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.SmtpSettings)]
        [HttpGet]
        public IActionResult Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }
        [Auth("Create", AuthPage.SmtpSettings)]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            if (fc.Files["pictures"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["pictures"]);
                model.Event.FilePath = imageResult.Path;
            }
            model.Event.FriendlyUrl = FriendlyUrl.FriendlyURLTitle(model.Event.Title);

            var result = await _eventRepository.AddAsync(model.Event);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/events");
        }


        [Auth("Update", AuthPage.SmtpSettings)]
        [HttpGet]
        public IActionResult Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.Event = _eventRepository.Get(x => x.ItemGuid == id).Result.Data;
            return View(model);
        }
        [Auth("Update", AuthPage.SmtpSettings)]
        [HttpPost]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = _eventRepository.Get(x => x.ItemGuid == model.Event.ItemGuid).Result.Data;
            if (currentItem != null)
            {
                if (fc.Files["pictures"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["pictures"]);
                    model.Event.FilePath = imageResult.Path;
                }
                model.Event.FriendlyUrl = FriendlyUrl.FriendlyURLTitle(model.Event.Title);
                base.Equalize(currentItem, model.Event);
                var result = await _eventRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/events");
            }
            return View();
        }
    }
}
