using Azure.Messaging;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Mail;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Controllers
{
    public class contactController : Controller
    {

        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IContactCategoryRepository _contactCategoryRepository;
        private readonly IExchangeRepository _exchangeRepository;

        public contactController(IContactMessageRepository contactMessageRepository, IMemoryCache memoryCache, IContactCategoryRepository contactCategoryRepository, IExchangeRepository exchangeRepository)
        {
            _contactMessageRepository = contactMessageRepository;
            _memoryCache = memoryCache;
            _contactCategoryRepository = contactCategoryRepository;
            _exchangeRepository = exchangeRepository;
        }

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.ContactCategoriesList = (await _contactCategoryRepository.GetListAsync()).Data;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ServiceVM model, IFormCollection fc)
        {
            try
            {
                var category = (await _contactCategoryRepository.GetListAsync(x => x.ItemGuid == fc["Category"].ToString())).Data.FirstOrDefault();
                if (category == null)
                {
                    return Json(false);
                }


                
                var contactMessage = new ContactMessages
                {
                    LangId = Int32.Parse(fc["LangId"].ToString()),
                    CategoryGuid = fc["Category"].ToString(),
                    Name = fc["Name"].ToString(),
                    Surname = fc["Surname"].ToString(),
                    Mail = fc["Mail"].ToString(),
                    Phone = fc["Phone"].ToString(),
                    Message = fc["Message"].ToString(),
                    IpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                    IsShow = false,
                    Count = Int32.Parse(fc["Count"].ToString()),
                    ExchangeGuid = fc["ExchangeGuid"].ToString() ?? "",
                    Reason = category.Name
                };

                var result = await _contactMessageRepository.AddAsync(contactMessage);
                if (result.Success)
                {

                    var title = "Yeni Mesaj";
                    Exchanges exc = null;
                    if (fc["ExchangeGuid"].ToString() != null || fc["ExchangeGuid"].ToString() != "")
                    {
                        exc = (await _exchangeRepository.Get(x => x.ItemGuid == fc["ExchangeGuid"].ToString())).Data;
                        if (exc != null)
                        {
                            title = "Eine neue Nachricht für " + exc.Name;
                        }
                        
                    }

                    MailHelper mHelper = new MailHelper();
                    mHelper.SendMail(title, contactMessage.Name + " " + contactMessage.Surname, "info@vermogenscheck24.de", HttpContext, contactMessage,exc);


                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        
    }
}
