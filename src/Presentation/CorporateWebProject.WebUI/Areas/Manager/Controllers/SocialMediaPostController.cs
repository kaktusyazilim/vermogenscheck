using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Repositories.SocialMediaPost;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class SocialMediaPostController : BaseController<SocialMediaPosts>
    {

        private readonly ISocialMediaPostRepository _socialMediaPostRepository;
        private readonly IMemoryCache _memoryCache;
        public SocialMediaPostController(ISocialMediaPostRepository socialMediaPostRepository, IMemoryCache memoryCache) : base(socialMediaPostRepository, AuthPage.SmtpSettings)
        {
            _socialMediaPostRepository = socialMediaPostRepository;
            _memoryCache = memoryCache;
        }

        [Auth("Read", AuthPage.SmtpSettings)]
        public IActionResult Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }

        [Auth("Create", AuthPage.SmtpSettings)]
        [HttpGet]
        public IActionResult Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }
        [Auth("Create", AuthPage.SmtpSettings)]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            return View();
        }


        [Auth("Update", AuthPage.SmtpSettings)]
        [HttpGet]
        public IActionResult Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }
        [Auth("Update", AuthPage.SmtpSettings)]
        [HttpPost]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            return View();
        }
    }
}
