using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class careersController : BaseController<Careers>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IMemoryCache _memoryCache;
        public careersController(ICareerRepository careerRepository, IMemoryCache memoryCache) : base(careerRepository, AuthPage.Careers)
        {
            _careerRepository = careerRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.Careers)]
        public IActionResult Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.Careers)]
        public IActionResult Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Careers)]
        public IActionResult Create(ServiceVM model, IFormCollection fc)
        {
            return View(model);
        }

        [HttpGet]
        [Auth("Update", AuthPage.Careers)]
        public IActionResult Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Careers)]
        public IActionResult Update(ServiceVM model, IFormCollection fc)
        {
            return View(model);
        }
    }
}
