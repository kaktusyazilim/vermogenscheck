using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class personnelsController : BaseController<Users>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        public personnelsController(IUserRepository userRepository, IMemoryCache memoryCache) : base(userRepository, AuthPage.Users)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        [Auth("Read", AuthPage.Users)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.UserList = (await _userRepository.GetListAsync()).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.Users)]
        [HttpGet]
        public IActionResult Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }
        [Auth("Create", AuthPage.Users)]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            return View();
        }


        [Auth("Update", AuthPage.Users)]
        [HttpGet]
        public IActionResult Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }
        [Auth("Update", AuthPage.Users)]
        [HttpPost]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            return View();
        }
    }
}
