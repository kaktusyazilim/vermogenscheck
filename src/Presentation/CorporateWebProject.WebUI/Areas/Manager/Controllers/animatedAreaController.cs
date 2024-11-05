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
    public class animatedAreaController : BaseController<Contents>
    {
        private readonly IContentRepository _contentRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IMemoryCache _memoryCache;
        public animatedAreaController(IContentRepository contentRepository, IMemoryCache memoryCache, ILanguageRepository languageRepository) : base(contentRepository, AuthPage.AnimatedArea)
        {
            _contentRepository = contentRepository;
            _memoryCache = memoryCache;
            _languageRepository = languageRepository;
        }


        [HttpGet]
        [Auth("Read", AuthPage.AnimatedArea)]

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.ContentsList = (await _contentRepository.GetListAsync(x => x.IsDeleted == false && x.ContentKey == "AnimatedArea")).Data;
            model.LanguageList = (await _languageRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            
            return View(model);
        }


        [HttpGet]
        [Auth("Read", AuthPage.AnimatedArea)]

        public IActionResult Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Contents.ContentKey = "AnimatedArea";
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.AnimatedArea)]

        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            try
            {
                var result = await _contentRepository.AddAsync(model.Contents);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/AnimatedArea");
            }
            catch (Exception)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/AnimatedArea");

            }
        }

        [HttpGet]
        [Auth("Read", AuthPage.AnimatedArea)]
        public async Task<IActionResult> Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Contents = (await _contentRepository.Get(x => x.ContentKey == "AnimatedArea" && x.ItemGuid == id)).Data;
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.AnimatedArea)]

        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            try
            {
                var currentModel = (await _contentRepository.Get(x => x.ItemGuid == model.Contents.ItemGuid)).Data;
                if (currentModel != null)
                {
                    currentModel.Text = model.Contents.Text ?? currentModel.Text;
                    currentModel.LongText = model.Contents.LongText ?? currentModel.LongText;
                    currentModel.Link = model.Contents.Link ?? currentModel.Link;
                    currentModel.Icon = model.Contents.Icon ?? currentModel.Icon;
                    base.Equalize(model, currentModel);

                    var result = await _contentRepository.UpdateAsync(currentModel);
                    base.SetResponseMessage(result.Success);
                    return Redirect("/manager/AnimatedArea");


                }
                base.SetResponseMessage(false);
                return Redirect("/manager/AnimatedArea");
            }
            catch (Exception ex)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/AnimatedArea");

            }
        }
    }
}
