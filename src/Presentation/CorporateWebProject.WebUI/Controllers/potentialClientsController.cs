
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.Mail;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Linq;

namespace Corporate.WebUI.Controllers
{
    public class potentialClientsController : Controller
    {
        private readonly ISmtpSettingRepository _smtpSettingRepository;
        private readonly MailManager _mailManager = new MailManager();
        private readonly IMemoryCache _memoryCache;

        public potentialClientsController(IMemoryCache memoryCache, ISmtpSettingRepository smtpSettingRepository)
        {
            _memoryCache = memoryCache;
            _smtpSettingRepository = smtpSettingRepository;
        }

        //public IActionResult Index(GoogleLeads lead)
        //{
        //    ServiceVM model = new ServiceVM(HttpContext);
        //    var smtpSetting = _smtpSettingsService.GetList().Data.Last();
        //    _mailManager.SendProposalForm(new ProposalDTO()
        //    {
        //        BrandName = "",
        //        Message = JsonConvert.SerializeObject(lead),
        //        Money = "",
        //        Name ="",
        //        Services = "",
        //        Surname = "",
        //        Phone ="",
        //        Mail = "",
        //        Tehnical = "",
        //        Website = "",
        //        Target = "",
        //    }, smtpSetting, model.Setting);
        //    return Json(lead);
        //}
    }
}
