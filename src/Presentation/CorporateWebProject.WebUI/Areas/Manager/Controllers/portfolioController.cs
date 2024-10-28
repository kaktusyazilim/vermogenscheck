using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Repositories.PortfolioCategoryMap;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class portfolioController : BaseController<Portfolio>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPortfolioCategoryMapRepository _portfolioCategoryMapRepository;
        private readonly IMemoryCache _memoryCache;

        public portfolioController(IMemoryCache memoryCache, IPortfolioCategoryMapRepository portfolioCategoryMapRepository, IPortfolioRepository portfolioRepository):base(portfolioRepository, AuthPage.Portfolio)
        {
            _memoryCache = memoryCache;
            _portfolioCategoryMapRepository = portfolioCategoryMapRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Auth("Read", AuthPage.Portfolio)]
        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadUserDataAsync(HttpContext);
            await model.LoadModulAndPageDataAsync(HttpContext);
            model.PortfolioList= (await _portfolioRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.PortfolioCategoryMapList = (await _portfolioCategoryMapRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View();
        }

        [HttpGet]
        [Auth("Create", AuthPage.Portfolio)]
        public async Task<IActionResult> Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadUserDataAsync(HttpContext);
            await model.LoadModulAndPageDataAsync(HttpContext);
            model.Portfolio = new Portfolio();
            model.PortfolioCategoryMapList = (await _portfolioCategoryMapRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Portfolio)]
        public async Task<IActionResult> Create(ServiceVM model)
        {
            var result = await _portfolioRepository.AddAsync(model.Portfolio);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/portfolio");
        }

       [HttpGet]
        [Auth("Update", AuthPage.Portfolio)]
        public async Task<IActionResult> Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadUserDataAsync(HttpContext);
            await model.LoadModulAndPageDataAsync(HttpContext);
            model.Portfolio = (await _portfolioRepository.Get(x => x.ItemGuid == id)).Data;
            model.PortfolioCategoryMapList = (await _portfolioCategoryMapRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Portfolio)]
        public async Task<IActionResult> Update(ServiceVM model)
        {
            var currentItem= (await _portfolioRepository.Get(x => x.ItemGuid == model.Portfolio.ItemGuid)).Data;
            base.Equalize(currentItem, model.Portfolio);
            var result = await _portfolioRepository.UpdateAsync(model.Portfolio);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/portfolio");
        }

        
    }
}
