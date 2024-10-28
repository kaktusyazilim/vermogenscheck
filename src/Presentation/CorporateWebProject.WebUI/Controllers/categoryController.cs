using CorporateWebProject.Application.Repositories;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.WebUI.Controllers
{
    public class categoryController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMemoryCache _memoryCache;

        public categoryController(IBlogRepository blogRepository, IMemoryCache memoryCache)
        {
            _blogRepository = blogRepository;
            _memoryCache = memoryCache;
        }

        [Route("kategori/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.Blog = (await _blogRepository.Get(x => x.FriendlyUrl == id)).Data;
            return View(model);
        }
    }
}
