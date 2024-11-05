using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class policyController : BaseController<Contents>
    {
        private readonly IContentRepository _contentRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IMemoryCache _memoryCache;

        public policyController(IContentRepository contentRepository, ILanguageRepository languageRepository, IMemoryCache memoryCache) : base(contentRepository, AuthPage.Policy)
        {
            _contentRepository = contentRepository;
            _languageRepository = languageRepository;
            _memoryCache = memoryCache;
        }

        
        [HttpGet]
        [Auth("Read", AuthPage.Policy)]

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.ContentsList = (await _contentRepository.GetListAsync(x => x.IsDeleted == false && x.ContentKey == "Policy")).Data;
            model.LanguageList = (await _languageRepository.GetListAsync(x => x.IsDeleted == false)).Data;

            return View(model);
        }


        [HttpGet]
        [Auth("Read", AuthPage.Policy)]

        public IActionResult Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Contents = new Contents();
            model.Contents.ContentKey = "Policy";
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.Policy)]

        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            try
            {
                model.Contents.ContentKey = "Policy";
                var result = await _contentRepository.AddAsync(model.Contents);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/Policy");
            }
            catch (Exception)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/Policy");

            }
        }

        [HttpGet]
        [Auth("Read", AuthPage.Policy)]
        public async Task<IActionResult> Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Contents = (await _contentRepository.Get(x => x.ContentKey == "Policy" && x.ItemGuid == id)).Data;
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.Policy)]

        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            try
            {
                var currentModel = (await _contentRepository.Get(x => x.ItemGuid == model.Contents.ItemGuid)).Data;
                if (currentModel != null)
                {
                    currentModel.Text = model.Contents.Text ?? currentModel.Text;
                    currentModel.Link = model.Contents.Link ?? currentModel.Link;

                    base.Equalize(model, currentModel);

                    var result = await _contentRepository.UpdateAsync(currentModel);
                    base.SetResponseMessage(result.Success);
                    return Redirect("/manager/Policy");


                }
                base.SetResponseMessage(false);
                return Redirect("/manager/Policy");
            }
            catch (Exception ex)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/Policy");

            }
        }
    }
}
