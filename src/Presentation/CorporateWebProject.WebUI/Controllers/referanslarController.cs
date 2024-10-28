
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Repositories.PortfolioCategoryMap;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.WebUI.Controllers
{
    public class referanslarController : Controller
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPortfolioCategoryMapRepository _portfolioCategoryMapRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IServiceRepository _serviceRepository;

        public referanslarController(IMemoryCache memoryCache, IPortfolioCategoryMapRepository portfolioCategoryMapRepository, IPortfolioRepository portfolioRepository, ICategoryRepository categoryRepository, IServiceRepository serviceRepository)
        {
            _memoryCache = memoryCache;
            _portfolioCategoryMapRepository = portfolioCategoryMapRepository;
            _portfolioRepository = portfolioRepository;
            _categoryRepository = categoryRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.PortfolioCategoryMapList = (await _portfolioCategoryMapRepository.GetListAsync()).Data;
            model.PortfolioList = (await _portfolioRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderByDescending(x => x.CreateDate).ToList();
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }
        [Route("referanslar/{id}")]
        public async Task<IActionResult> details(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.Portfolio = (await _portfolioRepository.Get(x => x.FriendlyUrl == id)).Data;
            model.PortfolioCategoryMapList = (await _portfolioCategoryMapRepository.GetListAsync(x => x.PortfolioFriendlyUrl == id)).Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;

            model.PortfolioList = (await _portfolioRepository.GetListAsync(x => x.Id != model.Portfolio.Id)).Data.OrderBy(x => x.Queue).ToList();
            return View(model);
        }
    }
}
