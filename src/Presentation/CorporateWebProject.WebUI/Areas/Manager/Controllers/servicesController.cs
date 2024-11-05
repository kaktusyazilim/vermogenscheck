using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class servicesController : BaseController<Services>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IMemoryCache _memoryCache;
        public servicesController(IServiceRepository serviceRepository, ICategoryRepository categoryRepository, ITypeRepository typeRepository, IMemoryCache memoryCache) : base(serviceRepository, AuthPage.Services)
        {
            _serviceRepository = serviceRepository;
            _categoryRepository = categoryRepository;
            _typeRepository = typeRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.Services)]
        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.Services)]
        public async Task<IActionResult> Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Type = (await _typeRepository.Get(x => x.ModulId == model.Modul.Id)).Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false && x.TypeId == model.Type.Id)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Services)]
        public IActionResult Create(ServiceVM model, IFormCollection fc)
        {
            return View(model);
        }

        [HttpGet]
        [Auth("Update", AuthPage.Services)]
        public IActionResult Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Services)]
        public IActionResult Update(ServiceVM model, IFormCollection fc)
        {
            return View(model);
        }
    }
}
