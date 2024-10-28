using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Repositories.SocialMediaPost;
using CorporateWebProject.Persistence.Repositories;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.WebUI.Controllers
{
    public class blogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IServiceRepository _serviceRepository;
        public blogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, IMemoryCache memoryCache, ISocialMediaRepository socialMediaRepository, IServiceRepository serviceRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _memoryCache = memoryCache;
            _socialMediaRepository = socialMediaRepository;
            _serviceRepository = serviceRepository;
        }
        public async Task<IActionResult> Index(int sayfa = 1, string category = "", string searchText = "")
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            int splitter = 5;

            if (category != "")
            {
                model.Category = (await _categoryRepository.Get(x => x.FriendlyUrl == category && x.TypeId == 14)).Data;
                model.BlogList = (await _blogRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false && x.CategoryId == model.Category.Id)).Data.OrderByDescending(x => x.CreateDate).ToList();

            }
            else
            {
                model.BlogList = (await _blogRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderByDescending(x => x.CreateDate).ToList();

            }
            if (searchText != null && searchText != "" && searchText.Length > 0)
            {
                model.BlogList = model.BlogList.Where(x => x.MetaDescription != null && x.FullDescription != null && x.ShortDescription != null && x.Tags != null).Where(x => x.FullDescription.Contains(searchText) || x.ShortDescription.Contains(searchText) || x.MetaDescription.Contains(searchText) || x.Tags.Contains(searchText)).ToList();
                ViewBag.SearchText = searchText;
            }
            ViewBag.BlogCount = model.BlogList.Count();
            ViewBag.CurrentPage = sayfa;
            ViewBag.Splitter = splitter;
            model.BlogList = sayfa == 1 ? model.BlogList.OrderByDescending(x => x.CreateDate).Take(splitter).ToList() : model.BlogList.OrderByDescending(x => x.CreateDate).Skip((sayfa - 1) * splitter).Take(splitter).ToList();
            model.UserList = (await _userRepository.GetListAsync()).Data;
            model.SocialMediaList = (await _socialMediaRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }
        [HttpGet("blog/{id}")]
        public async Task<IActionResult> details(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.Blog = (await _blogRepository.Get(x => x.FriendlyUrl == id)).Data;
            model.BlogList = (await _blogRepository.GetListAsync(x => x.Id != model.Blog.Id)).Data.OrderByDescending(x => x.CreateDate).ToList();
            model.User = (await _userRepository.Get(x => x.Id == model.Blog.CreatedUserId)).Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }
    }
}
