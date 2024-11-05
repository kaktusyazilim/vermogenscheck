using CorporateWebProject.Application.Repositories;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Controllers
{
    public class formController : Controller
    {

        private readonly IContactCategoryRepository _contactCategoryRepository;
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IMemoryCache _memoryCache;

        public formController(IContactCategoryRepository contactCategoryRepository, IExchangeRepository exchangeRepository, IMemoryCache memoryCache)
        {
            _contactCategoryRepository = contactCategoryRepository;
            _exchangeRepository = exchangeRepository;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index(string Exchange)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            
            model.Exchanges = (await _exchangeRepository.Get(x => x.ItemGuid == Exchange)).Data;
            if (model.Exchanges == null)
            {
                return Redirect("/contact");
            }
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.ContactCategoriesList = (await _contactCategoryRepository.GetListAsync()).Data;
            model.IsFooterActive = false;
            return View(model);
        }
    }
}
