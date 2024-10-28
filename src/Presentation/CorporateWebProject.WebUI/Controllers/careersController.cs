using CorporateWebProject.Application.Repositories;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Corporate.WebUI.Controllers
{
    public class careersController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IServiceRepository _serviceRepository;

        public careersController(IMemoryCache memoryCache, ICategoryRepository categoryRepository, IServiceRepository serviceRepository)
        {
            _memoryCache = memoryCache;
            _categoryRepository = categoryRepository;
            _serviceRepository = serviceRepository;
        }

        [Route("kariyer")]
        [Route("career")]

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.Category = (await _categoryRepository.Get(x => x.FriendlyUrl == "kariyer")).Data;
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }


        [Route("kariyer/{id}")]
        public async Task<IActionResult> details(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.Category = (await _categoryRepository.Get(x => x.FriendlyUrl == "kariyer")).Data;
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }
    }
}
