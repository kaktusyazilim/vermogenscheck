using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class currencyController : BaseController<SmtpSettings>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMemoryCache _memoryCache;

        public currencyController(ICurrencyRepository currencyRepository, IMemoryCache memoryCache) : base(currencyRepository, AuthPage.SmtpSettings)
        {
            _currencyRepository = currencyRepository;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Auth("Read", AuthPage.Subcategory)]
        public async Task<IActionResult> GetData()
        {
            try
            {
                var data = (await _currencyRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x => x.Queue).ToList();
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
