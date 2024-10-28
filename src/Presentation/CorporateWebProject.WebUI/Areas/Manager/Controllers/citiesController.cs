using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class citiesController : BaseController<Cities>
    {
        private readonly ICityRepository _citiesRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IMemoryCache _memoryCache;

        public citiesController(ICityRepository citiesRepository, IDistrictRepository districtRepository, IMemoryCache memoryCache) : base(citiesRepository, AuthPage.Cities)
        {
            _citiesRepository = citiesRepository;
            _districtRepository = districtRepository;
            _memoryCache = memoryCache;
        }

        [Auth("All", AuthPage.Cities)]
        public async Task<IActionResult> GetData()
        {
            var data = (await _citiesRepository.GetListAsync()).Data;
            return Json(data);
        }
        [Auth("All",AuthPage.Cities)]
        public async Task<IActionResult> GetDistrictData(int cityid)
        {
            var data = (await _districtRepository.GetListAsync(x => x.City == cityid)).Data;
            return Json(data);
        }
    }
}
