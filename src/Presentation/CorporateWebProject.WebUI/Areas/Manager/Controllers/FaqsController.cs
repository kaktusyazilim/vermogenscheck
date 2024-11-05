using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class FaqsController : BaseController<Faq>
    {
        private readonly IFaqRepository _faqRepository;
        private readonly IMemoryCache _memoryCache;
        public FaqsController(IFaqRepository faqRepository, IMemoryCache memoryCache) : base(faqRepository, AuthPage.Faqs)
        {
            _faqRepository = faqRepository;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Auth("Read", AuthPage.Faqs)]

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.FaqList = (await _faqRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }


        [HttpGet]
        [Auth("Read", AuthPage.Faqs)]

        public IActionResult Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.Faqs)]

        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            try
            {
                
                model.Faq.Queue = model.Faq.Queue == null ? 0 : model.Faq.Queue;
                model.Faq.Question = model.Faq.Question ?? "";
                model.Faq.Answer = model.Faq.Answer ?? "";
                model.Faq.IsShowHome = model.Faq.IsShowHome == null ? false : model.Faq.IsShowHome;

                var result = await _faqRepository.AddAsync(model.Faq);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/Faqs");
            }
            catch (Exception)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/Faqs");

            }
        }

        [HttpGet]
        [Auth("Read", AuthPage.Faqs)]
        public async Task<IActionResult> Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Faq = (await _faqRepository.Get(x => x.ItemGuid == id)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Read", AuthPage.Faqs)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            try
            {
                var currentModel = (await _faqRepository.Get(x => x.ItemGuid == model.Faq.ItemGuid)).Data;
                if (currentModel != null)
                {
                    
                    currentModel.Queue = model.Faq.Queue == null ? 0 : model.Faq.Queue;
                    currentModel.Question = model.Faq.Question ?? currentModel.Question;
                    currentModel.Answer = model.Faq.Answer ?? currentModel.Answer;
                    currentModel.IsShowHome = model.Faq.IsShowHome == null ? false : model.Faq.IsShowHome;

                    base.Equalize(model, currentModel);

                    var result = await _faqRepository.UpdateAsync(currentModel);
                    base.SetResponseMessage(result.Success);
                    return Redirect("/manager/Faqs");


                }
                base.SetResponseMessage(false);
                return Redirect("/manager/Faqs");
            }
            catch (Exception ex)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/Faqs");

            }
        }
    }
}
