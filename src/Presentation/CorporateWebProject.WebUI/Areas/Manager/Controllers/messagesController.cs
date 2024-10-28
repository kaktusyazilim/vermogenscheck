using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class messagesController : BaseController<SmtpSettings>
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IMemoryCache _memoryCache;
        public messagesController(IContactMessageRepository contactMessageRepository, IMemoryCache memoryCache) : base(contactMessageRepository, AuthPage.Messages)
        {
            _contactMessageRepository = contactMessageRepository;
            _memoryCache = memoryCache;
        }

        [Auth("Read", AuthPage.Messages)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.MessageList = (await _contactMessageRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [Auth("Delete", AuthPage.Messages)]
        public async Task<IActionResult> AllDelete()
        {
            var messages = (await _contactMessageRepository.GetListAsync()).Data;
            foreach (var item in messages)
            {
                await _contactMessageRepository.Delete(item);
            }
            base.SetResponseMessage(true);
            return Redirect("/manager/messages");
        }
    }
}
