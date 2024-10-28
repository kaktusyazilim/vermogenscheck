using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class slidersController : BaseController<Sliders>
    {
        private readonly IUserRepository userRepository;
        private readonly ISliderRepository _sliderRepository;
        private readonly IMemoryCache _memoryCache;
        public slidersController(IUserRepository userRepository, ISliderRepository sliderRepository, IMemoryCache memoryCache) : base(sliderRepository, AuthPage.Sliders)
        {
            this.userRepository = userRepository;
            _sliderRepository = sliderRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.Sliders)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.SliderList = (await _sliderRepository.GetListAsync(x => x.IsDeleted == false)).Data.OrderByDescending(x => x.CreateDate).ToList();
            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.Sliders)]
        public async Task<IActionResult> Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Auth("Create", AuthPage.Sliders)]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {

            if (fc.Files["files"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["files"]);
                model.Slider.FilePath = imageResult.Path;

                model.Slider.Button1Text = model.Slider.Button1Text ?? "";
                model.Slider.Button1Color = model.Slider.Button2Color ?? "";
                model.Slider.Button1Url = model.Slider.Button1Url ?? "";

                model.Slider.Button2Url = model.Slider.Button2Url ?? "";
                model.Slider.Button2Color = model.Slider.Button2Color ?? "";
                model.Slider.Button2Text = model.Slider.Button2Text ?? "";

                model.Slider.SubTitle = model.Slider.SubTitle ?? "";
                model.Slider.Title = model.Slider.Title == null ? "" : model.Slider.Title;
                model.Slider.TopTitle = model.Slider.TopTitle ?? "";
                var result = await _sliderRepository.AddAsync(model.Slider);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/sliders");
            }
            return View(model);



        }

        [HttpGet]
        [Auth("Update", AuthPage.Sliders)]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.Slider = (await _sliderRepository.Get(x => x.ItemGuid == id)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Sliders)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = _sliderRepository.Get(x => x.ItemGuid == model.Slider.ItemGuid).Result.Data;
            if (currentItem != null)
            {
                base.Equalize(currentItem, model.Slider);
                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    currentItem.FilePath = imageResult.Path;
                }

                var result = await _sliderRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/sliders");
            }
            return View(model);
        }
    }
}