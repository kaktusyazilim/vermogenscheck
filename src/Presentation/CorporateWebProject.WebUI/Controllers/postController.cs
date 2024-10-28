
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
    public class postController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;

        public postController(IMemoryCache memoryCache, IUserRepository userRepository, ICategoryRepository categoryRepository, IBlogRepository blogRepository)
        {
            _memoryCache = memoryCache;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _blogRepository = blogRepository;
        }

        [Route("post/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.Blog = (await _blogRepository.Get(x => x.FriendlyUrl == id)).Data;
            model.Category = (await _categoryRepository.Get(x => x.Id == model.Blog.CategoryId)).Data;
            model.User = (await _userRepository.Get(x => x.Id == model.Blog.CreatedUserId)).Data;
            return View(model);
        }
    }
}
