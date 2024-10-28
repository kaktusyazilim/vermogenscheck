using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.WebUI.Controllers
{
    public class companiesController1 : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public companiesController1(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [Route("firmalar")]
        [Route("companies")]
        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            return View(model);
        }
        [Route("firmalar/{id}")]
        [Route("companies/{id}")]
        public async Task<IActionResult> detail(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            return View(model);
        }
        [Route("firmalar/{id}/showroom")]
        [Route("companies/{id}/showroom")]
        public async Task<IActionResult> showroom(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            return View(model);
        }
    }
}
