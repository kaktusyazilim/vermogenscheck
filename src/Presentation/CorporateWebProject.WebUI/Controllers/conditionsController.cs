using CorporateWebProject.Application.Repositories;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Controllers
{
    public class conditionsController : Controller
    {

        private readonly IContentRepository _contentRepository;
        private readonly IMemoryCache _memoryCache;
        public conditionsController(IContentRepository contentRepository, IMemoryCache memoryCache)
        {
            _contentRepository = contentRepository;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.Contents = (await _contentRepository.GetListAsync(x => x.IsDeleted == false && x.ContentKey == "Policy")).Data.FirstOrDefault();
            return View(model);
        }

    }
}
