using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("manager")]
    public class banksController : BaseController<Banks>
    {
        private readonly IBankRepository _bankRepository;
        private readonly IMemoryCache _memoryCache;

        public banksController(IBankRepository bankRepository, IMemoryCache memoryCache) : base(bankRepository, AuthPage.Banks)
        {
            _bankRepository = bankRepository;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Auth("Read", AuthPage.Banks)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Auth("Create", AuthPage.Banks)]
        public IActionResult Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Banks)]
        public IActionResult Create(ServiceVM model, IFormCollection fc)
        {
            return View(model);
        }

        [HttpGet]
        [Auth("Update", AuthPage.Banks)]
        public IActionResult Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Banks)]
        public IActionResult Update(ServiceVM model, IFormCollection fc)
        {
            return View(model);
        }
    }
}
