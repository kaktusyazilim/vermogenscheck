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
    public class aboutUsController : BaseController<AboutUs>
    {
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IMemoryCache _memoryCache;

        public aboutUsController(IMemoryCache memoryCache, IAboutUsRepository aboutUsRepository) : base(aboutUsRepository, AuthPage.AboutUs)
        {
            _memoryCache = memoryCache;
            _aboutUsRepository = aboutUsRepository;
        }

        [HttpGet]
        [Auth("Read", AuthPage.AboutUs)]

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.AboutUsList = (await _aboutUsRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }


        [HttpGet]
        [Auth("Read", AuthPage.AboutUs)]

        public IActionResult Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.AboutUs)]

        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            try
            {
                if (fc.Files["pictures"] != null)
                {
                    var image = base.CreateFile(fc.Files["pictures"]);
                    model.AboutUs.Photo = image.Path;
                }
                var result = await _aboutUsRepository.AddAsync(model.AboutUs);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/aboutus");
            }
            catch (Exception)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/aboutus");

            }
        }

        [HttpGet]
        [Auth("Read", AuthPage.AboutUs)]
        public async Task<IActionResult> Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.AboutUs = (await _aboutUsRepository.Get(x => x.ItemGuid == id)).Data;
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.AboutUs)]

        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            try
            {
                var currentModel = (await _aboutUsRepository.Get(x => x.ItemGuid == model.AboutUs.ItemGuid)).Data;
                if (currentModel != null)
                {
                    if (fc.Files["pictures"] != null)
                    {
                        var image = base.CreateFile(fc.Files["pictures"]);
                        model.AboutUs.Photo = image.Path;
                    }
                    base.Equalize(model, currentModel);

                    var result = await _aboutUsRepository.UpdateAsync(currentModel);
                    base.SetResponseMessage(result.Success);
                    return Redirect("/manager/aboutus");


                }
                base.SetResponseMessage(false);
                return Redirect("/manager/aboutus");
            }
            catch (Exception ex)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/aboutus");

            }
        }
    }
}
