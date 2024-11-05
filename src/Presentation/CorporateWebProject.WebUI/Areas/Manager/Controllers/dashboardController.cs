using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.WebSettings;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("manager")]
    public class dashboardController : Controller
    {
        private readonly IModulRepository _modulRepository;



        private readonly IPageRepository _pageRepository;
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IVisitorLogRepository _visitorLogRepository;
        private readonly IVisitorLogDetailRepository _visitorLogDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;

        public dashboardController(IModulRepository modulRepository, IPageRepository pageRepository, IContactMessageRepository contactMessageRepository, IVisitorLogRepository visitorLogRepository, IVisitorLogDetailRepository visitorLogDetailRepository, IUserRepository userRepository, IMemoryCache memoryCache)
        {
            _modulRepository = modulRepository;
            _pageRepository = pageRepository;
            _contactMessageRepository = contactMessageRepository;
            _visitorLogRepository = visitorLogRepository;
            _visitorLogDetailRepository = visitorLogDetailRepository;
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        [Auth("Read", AuthPage.Dashboard)]
        public async Task<IActionResult> Index()
        {

            try
            {
                ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
                model.MessageList = (await _contactMessageRepository.GetListAsync()).Data;
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        public async Task<IActionResult> GetVisitorForDaysReport()
        {

            List<string> categories = new List<string>();
            List<string> data = new List<string>();
            var visitors = (await _visitorLogRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderByDescending(x => x.CreateDate).ToList();
            foreach (var item in visitors.GroupBy(x => x.CreateDate.Date).Take(30).ToList())
            {
                categories.Add(item.Key.ToString("dd MMMM"));
                data.Add(visitors.Where(x => x.CreateDate.Date == item.Key).GroupBy(x => x.IpAddress).Count().ToString());
            }
            string json = "{'categories':" + JsonConvert.SerializeObject(categories) + ",'data':" + JsonConvert.SerializeObject(data) + "}";
            json = json.Replace("'", @"""");
            return Json(json);
        }

        public async Task<IActionResult> GetVisitorForBrowsersReport()
        {

            List<string> labels = new List<string>();
            List<int> series = new List<int>();
            var visitors = (await _visitorLogRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderByDescending(x => x.CreateDate).ToList();
            foreach (var item in visitors.GroupBy(x => x.Browser))
            {
                if (item.Key != "" && item.Key != null)
                {
                    labels.Add(item.Key);
                    series.Add(visitors.Where(x => x.Browser == item.Key).Count());
                }

            }
            string json = "{'labels':" + JsonConvert.SerializeObject(labels) + ",'series':" + JsonConvert.SerializeObject(series) + "}";
            json = json.Replace("'", @"""");
            return Json(json);
        }

        public async Task<IActionResult> GetVisitorForLinksReport()
        {

            List<string> categories = new List<string>();
            List<int> data = new List<int>();
            var visitors = (await _visitorLogDetailRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.Where(x => !x.Url.Contains("css") || !x.Url.Contains("js")).OrderByDescending(x => x.CreateDate).ToList();
            var visitorsCounter = visitors.GroupBy(x => x.Url).Select(x => new { link = x.Key, count = visitors.Where(y => y.Url == x.Key).Count() }).OrderByDescending(x => x.count).Take(15).ToList();
            string json = "{'categories':" + JsonConvert.SerializeObject(visitorsCounter.Select(x => x.link).ToArray()) + ",'data':" + JsonConvert.SerializeObject(visitorsCounter.Select(x => x.count).ToArray()) + "}";
            json = json.Replace("'", @"""");
            return Json(json);
        }


        public async Task<IActionResult> GetVisitorForCountryReport()
        {

            List<string> categories = new List<string>();
            List<int> data = new List<int>();
            var visitors = (await _visitorLogRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderByDescending(x => x.CreateDate).ToList();
            var visitorsCounter = visitors.GroupBy(x => x.Country).Select(x => new { link = x.Key == null ? "Bilinmiyor" : x.Key, count = visitors.Where(y => y.Country == x.Key).Count() }).OrderByDescending(x => x.count).Take(15).ToList();
            string json = "{'labels':" + JsonConvert.SerializeObject(visitorsCounter.Select(x => x.link).ToArray()) + ",'series':" + JsonConvert.SerializeObject(visitorsCounter.Select(x => x.count).ToArray()) + "}";
            json = json.Replace("'", @"""");
            return Json(json);
        }

        public async Task<IActionResult> GetVisitorForCityReport()
        {

            List<string> categories = new List<string>();
            List<int> data = new List<int>();
            var visitors = (await _visitorLogRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderByDescending(x => x.CreateDate).ToList();
            var visitorsCounter = visitors.GroupBy(x => x.Location).Select(x => new { link = x.Key, count = visitors.Where(y => y.Location == x.Key).Count() }).OrderByDescending(x => x.count).Take(15).ToList();
            string json = "{'labels':" + JsonConvert.SerializeObject(visitorsCounter.Select(x => x.link).ToArray()) + ",'series':" + JsonConvert.SerializeObject(visitorsCounter.Select(x => x.count).ToArray()) + "}";
            json = json.Replace("'", @"""");
            return Json(json);
        }


        public async Task<IActionResult> GetVisitorForIpReport()
        {

            List<string> categories = new List<string>();
            List<int> data = new List<int>();
            var visitors = (await _visitorLogDetailRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderByDescending(x => x.CreateDate).ToList();
            var visitorsCounter = visitors.GroupBy(x => x.IpAddress).Select(x => new { link = x.Key, count = visitors.Where(y => y.IpAddress == x.Key).Count() }).OrderByDescending(x => x.count).Take(15).ToList();
            string json = "{'categories':" + JsonConvert.SerializeObject(visitorsCounter.Select(x => x.link).ToArray()) + ",'data':" + JsonConvert.SerializeObject(visitorsCounter.Select(x => x.count).ToArray()) + "}";
            json = json.Replace("'", @"""");
            return Json(json);
        }

        public async Task<IActionResult> GetContactForMonthReport()
        {

            var contactMessage = (await _contactMessageRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;

            var contactCounter = contactMessage.GroupBy(x => x.CreateDate.Month).Select(x => new { Month = GetMonth(x.Key), Count = contactMessage.Where(y => y.CreateDate.Month == x.Key).Count() }).OrderByDescending(x => x.Month).ToList();
            string json = "{'categories':" + JsonConvert.SerializeObject(contactCounter.Select(x => x.Month).ToArray()) + ",'data':" + JsonConvert.SerializeObject(contactCounter.Select(x => x.Count).ToArray()) + "}";
            json = json.Replace("'", @"""");
            return Json(json);
        }

        public string GetMonth(int month)
        {
            var DateTime = new DateTime(2020, month, 1, 1, 1, 1);
            return DateTime.ToString("MMMM");
        }
        public int monthtoint(string month)
        {
            switch (month.ToLower())
            {
                case "ocak":
                    return 1;
                case "şubat":
                    return 2;
                case "mart":
                    return 3;
                case "nisan":
                    return 4;
                case "mayıs":
                    return 5;
                case "haziran":
                    return 6;
                case "temmuz":
                    return 7;
                case "ağustos":
                    return 8;
                case "eylül":
                    return 9;
                case "ekim":
                    return 10;
                case "kasım":
                    return 11;
                case "aralık":
                    return 12;

            }
            return 0;
        }

    }
}
