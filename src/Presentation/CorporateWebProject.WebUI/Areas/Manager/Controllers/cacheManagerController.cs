using Aspose.Imaging;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.Cache;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class cacheManagerController : BaseController<Users>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        public cacheManagerController(IUserRepository userRepository, IMemoryCache memoryCache) : base(userRepository, AuthPage.Users)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }
        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadModulAndPageDataAsync(HttpContext);
            await model.LoadUserDataAsync(HttpContext);
            model.StringList = CacheManager.GetCacheKeys();
            return View(model);
        }

        public IActionResult RemoveCache(string name)
        {
            var cacheKey = CacheManager.GetCacheKeys().FirstOrDefault(x=>x==name);
            if (cacheKey != null)
            {
                CacheManager.RemoveCache(_memoryCache, cacheKey);
            }
            return Redirect("/manager/cacheManager");

        }

        public IActionResult RemoveAllCache()
        {
            CacheManager.ClearAllCache(_memoryCache);
            return Redirect("/manager/cacheManager");
        }
    }
}

