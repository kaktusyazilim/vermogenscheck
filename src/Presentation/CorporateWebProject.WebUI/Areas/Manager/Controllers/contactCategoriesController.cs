using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class contactCategoriesController : BaseController<ContactCategories>
    {

        private readonly IContactCategoryRepository _contactCategoryRepository;
        private readonly IMemoryCache _memoryCache;
        public contactCategoriesController(IMemoryCache memoryCache, IContactCategoryRepository contactCategoryRepository) : base(contactCategoryRepository, AuthPage.ContactCategories)
        {
            _memoryCache = memoryCache;
            _contactCategoryRepository = contactCategoryRepository;
        }



        [HttpGet]
        [Auth("Read", AuthPage.ContactCategories)]

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.ContactCategoriesList = (await _contactCategoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }


        [HttpGet]
        [Auth("Read", AuthPage.ContactCategories)]

        public IActionResult Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.ContactCategories)]

        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            try
            {
                model.ContactCategories.Name = model.ContactCategories.Name ?? "";

                var result = await _contactCategoryRepository.AddAsync(model.ContactCategories);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/ContactCategories");
            }
            catch (Exception)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/ContactCategories");

            }
        }

        [HttpGet]
        [Auth("Read", AuthPage.ContactCategories)]
        public async Task<IActionResult> Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.ContactCategories = (await _contactCategoryRepository.Get(x => x.ItemGuid == id)).Data;
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.ContactCategories)]

        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            try
            {
                var currentModel = (await _contactCategoryRepository.Get(x => x.ItemGuid == model.ContactCategories.ItemGuid)).Data;
                if (currentModel != null)
                {
                    currentModel.Name = model.ContactCategories.Name ?? currentModel.Name;

                    base.Equalize(model, currentModel);

                    var result = await _contactCategoryRepository.UpdateAsync(currentModel);
                    base.SetResponseMessage(result.Success);
                    return Redirect("/manager/ContactCategories");


                }
                base.SetResponseMessage(false);
                return Redirect("/manager/ContactCategories");
            }
            catch (Exception ex)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/ContactCategories");

            }
        }
    }
}
