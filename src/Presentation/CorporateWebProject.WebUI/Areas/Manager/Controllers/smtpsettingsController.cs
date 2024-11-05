using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class smtpsettingsController : BaseController<SmtpSettings>
    {
        private readonly ISmtpSettingRepository _smtpSettingRepository;
        private readonly IMemoryCache _memoryCache;

        public smtpsettingsController(ISmtpSettingRepository smtpSettingRepository, IMemoryCache memoryCache) : base(smtpSettingRepository, AuthPage.SmtpSettings)
        {
            _smtpSettingRepository = smtpSettingRepository;
            _memoryCache = memoryCache;
        }

        [Auth("Read",AuthPage.SmtpSettings)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.SmtpSettingList = (await _smtpSettingRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.SmtpSettings)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }
        [Auth("Create", AuthPage.SmtpSettings)]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceVM model,IFormCollection fc)
        {
            return View();
        }


        [Auth("Update", AuthPage.SmtpSettings)]
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.SmtpSetting = _smtpSettingRepository.Get(x => x.ItemGuid == id).Result.Data;
            return View(model);
        }
        [Auth("Update", AuthPage.SmtpSettings)]
        [HttpPost]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            return View();
        }
    }
}
