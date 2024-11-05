using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class accountingController : BaseController<SmtpSettings>
    {
        private readonly ISmtpSettingRepository _smtpSettingRepository;
        private readonly IMemoryCache _memoryCache;

        public accountingController(ISmtpSettingRepository smtpSettingRepository, IMemoryCache memoryCache) : base(smtpSettingRepository, AuthPage.SmtpSettings)
        {
            _smtpSettingRepository = smtpSettingRepository;
            _memoryCache = memoryCache;
        }

        [Auth("Read", AuthPage.SmtpSettings)]
        public IActionResult advanceRequest() // Avans Talep Et
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }

        [Auth("Read", AuthPage.SmtpSettings)]
        public IActionResult creditRequest() // Kredi Kartı Çekim Talep Et
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }
        [Auth("Read", AuthPage.SmtpSettings)]
        public IActionResult sendAccountNumber() // Hesap Numarası Gönder
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }


    }
}
