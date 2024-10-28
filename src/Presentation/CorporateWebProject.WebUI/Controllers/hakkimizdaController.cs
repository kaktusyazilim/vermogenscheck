using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Repositories.Team;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.WebUI.Controllers
{
    public class hakkimizdaController : Controller
    {
        private readonly ITeamRepository _teamRepository;



        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IServiceRepository _serviceRepository;

        public hakkimizdaController(ITeamRepository teamRepository, IPortfolioRepository portfolioRepository, IMemoryCache memoryCache, IServiceRepository serviceRepository)
        {
            _teamRepository = teamRepository;
            _portfolioRepository = portfolioRepository;
            _memoryCache = memoryCache;
            _serviceRepository = serviceRepository;
        }

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.TeamList = (await _teamRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x => x.Queue).ToList();
            model.PortfolioList = (await _portfolioRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x => x.Queue).ToList();
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }
    }
}
