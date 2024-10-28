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
    public class hizmetlerController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IWhyUsRepository _whyUsRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFaqRepository _faqRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IPacketRepository _packetRepository;
        private readonly IMemoryCache _memoryCache;

        public hizmetlerController(IPacketRepository packetRepository, IMemoryCache memoryCache, ITeamRepository teamRepository, IFaqRepository faqRepository, IPortfolioRepository portfolioRepository, IWhyUsRepository whyUsRepository, IServiceRepository serviceRepository)
        {
            _packetRepository = packetRepository;
            _memoryCache = memoryCache;
            _teamRepository = teamRepository;
            _faqRepository = faqRepository;
            _portfolioRepository = portfolioRepository;
            _whyUsRepository = whyUsRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data.OrderBy(x => x.Queue).ToList();
            model.WhyUsList = (await _whyUsRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            model.PortfolioList = (await _portfolioRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            model.TeamList = (await _teamRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data.OrderBy(x => x.Id).ToList();
            return View(model);
        }

        [HttpGet("hizmetler/{id}")]
        public async Task<IActionResult> details(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.Service = (await _serviceRepository.Get(x => x.FriendlyUrl == id)).Data;
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.Id != model.Service.Id && x.IsDeleted == false && x.IsPassive == false)).Data.OrderBy(x => x.Queue).ToList();
            model.PortfolioList = (await _portfolioRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.Service = (await _serviceRepository.Get(x => x.FriendlyUrl == id)).Data;
            model.FaqList = (await _faqRepository.GetListAsync(x => x.CategoryId == model.Service.Id && x.IsDeleted == false && x.IsPassive == false)).Data;
            model.PacketList = (await _packetRepository.GetListAsync(x => x.ServiceId == model.Service.Id && x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x => x.Queue).ToList();
            
            return View(model);
        }
    }
}
