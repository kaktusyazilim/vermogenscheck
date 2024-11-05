using Aspose.Imaging;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Repositories.Brands;
using CorporateWebProject.Application.Repositories.PortfolioCategoryMap;
using CorporateWebProject.Application.Utilities.WebSettings;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.Persistence.Repositories;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace CorporateWebProject.WebUI.Controllers
{
    public class mainController : Controller
    {
        private readonly ITypeRepository _typeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<mainController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IBrandRepository _brandRepository;
        private readonly IWhyUsRepository _whyUsRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPortfolioCategoryMapRepository _portfolioCategoryMapRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IFaqRepository _faqRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IContentRepository _contentRepository;

        public mainController(ITypeRepository typeRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IEventRepository eventRepository, ICommentRepository commentRepository, ILogger<mainController> logger, IMemoryCache memoryCache, IBrandRepository brandRepository, IWhyUsRepository whyUsRepository, IPortfolioRepository portfolioRepository, IPortfolioCategoryMapRepository portfolioCategoryMapRepository, IServiceRepository serviceRepository, IFaqRepository faqRepository, IUserRepository userRepository, ILanguageRepository languageRepository, IAboutUsRepository aboutUsRepository, IExchangeRepository exchangeRepository, IContentRepository contentRepository)
        {
            _typeRepository = typeRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _eventRepository = eventRepository;
            _commentRepository = commentRepository;
            _logger = logger;
            _memoryCache = memoryCache;
            _brandRepository = brandRepository;
            _whyUsRepository = whyUsRepository;
            _portfolioRepository = portfolioRepository;
            _portfolioCategoryMapRepository = portfolioCategoryMapRepository;
            _serviceRepository = serviceRepository;
            _faqRepository = faqRepository;
            _userRepository = userRepository;
            _languageRepository = languageRepository;
            _aboutUsRepository = aboutUsRepository;
            _exchangeRepository = exchangeRepository;
            _contentRepository = contentRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.LangId == model.CurrentLanguage.Id && x.IsPassive == false && x.IsDeleted == false)).Data;
            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.LangId == model.CurrentLanguage.Id && x.IsPassive == false && x.IsDeleted == false)).Data;
            model.EventList = (await _eventRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.CommentList = (await _commentRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x => x.Queue).ToList();
            model.WhyUsList = (await _whyUsRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.BrandList = (await _brandRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.PortfolioList = (await _portfolioRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.PortfolioCategoryMapList = (await _portfolioCategoryMapRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.FaqList = (await _faqRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.UserList = (await _userRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.ExchangesList = (await _exchangeRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.FaqList = (await _faqRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.AboutUs = (await _aboutUsRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.FirstOrDefault();
            model.ContentsList = (await _contentRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {

            var language = _languageRepository.Get(x => x.Code == culture).Result.Data;
            if (language != null)
            {
                Response.Cookies.Delete("Language");
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddYears(1);
                string key = "Language";
                string value = language.Code;
                Response.Cookies.Append(key, value, cookie);
            }
            return Redirect(returnUrl);
        }

        //[Route("iletisim-onaylandi")]
        //public async Task<IActionResult> iletisimonaylandi()
        //{
        //    ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
        //    model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
        //    model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
        //    await model.LoadLanguageSettingsAsync(HttpContext);

        //    return View(model);
        //}
        //[Route("iletisime-gec")]
        //public async Task<IActionResult> iletisimegec()
        //{
        //    ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
        //    await model.LoadLanguageSettingsAsync(HttpContext);

        //    model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
        //    model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
        //    return View(model);
        //}

        //[Route("gizlilik")]
        //public async Task<IActionResult> gizlilik()
        //{
        //    ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
        //    await model.LoadLanguageSettingsAsync(HttpContext);
        //    model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
        //    model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
        //    return View(model);
        //}

        [Route("Cookie-Richtlinie")]
        public async Task<IActionResult> cerezpolitikasi()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }

        [Route("/sitemap.xml")]
        public async Task<IActionResult> Sitemap()
        {

            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.ExchangesList = (await _exchangeRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;

            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            string segment = "blog";
            string contentType = "application/xml";

            string cacheKey = "sitemap.xml";

            // For showing in browser (Without download)
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = cacheKey,
                Inline = true,
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());

            // Cache
            var bytes = _memoryCache.Get<byte[]>(cacheKey);
            if (bytes != null)
                return File(bytes, contentType);


            var sb = new StringBuilder();
            sb.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine($"<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"");
            sb.AppendLine($"   xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            sb.AppendLine($"   xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");

            sb.AppendLine($"    <url>");
            sb.AppendLine($"        <loc>{baseUrl}/</loc>");
            sb.AppendLine($"        <lastmod>{DateTime.Now.ToString("yyyy-MM-dd")}</lastmod>");
            sb.AppendLine($"        <changefreq>daily</changefreq>");
            sb.AppendLine($"        <priority>0.8</priority>");
            sb.AppendLine($"    </url>");

            sb.AppendLine($"    <url>");
            sb.AppendLine($"        <loc>{baseUrl}/privacy</loc>");
            sb.AppendLine($"        <lastmod>{DateTime.Now.ToString("yyyy-MM-dd")}</lastmod>");
            sb.AppendLine($"        <changefreq>daily</changefreq>");
            sb.AppendLine($"        <priority>0.8</priority>");
            sb.AppendLine($"    </url>");


            sb.AppendLine($"    <url>");
            sb.AppendLine($"        <loc>{baseUrl}/contact</loc>");
            sb.AppendLine($"        <lastmod>{DateTime.Now.ToString("yyyy-MM-dd")}</lastmod>");
            sb.AppendLine($"        <changefreq>daily</changefreq>");
            sb.AppendLine($"        <priority>0.8</priority>");
            sb.AppendLine($"    </url>");

            foreach (var item in model.ExchangesList)
            {
                sb.AppendLine($"<url>");
                sb.AppendLine($"<loc>{baseUrl}/form?Exchange={item.ItemGuid}</loc>");
                sb.AppendLine($"<lastmod>{DateTime.Now.ToString("yyyy-MM-dd")}</lastmod>");
                sb.AppendLine($"<changefreq>daily</changefreq>");
                sb.AppendLine($"<priority>0.8</priority>");
                sb.AppendLine($"</url>");
            }
            


            sb.AppendLine($"</urlset>");

            bytes = Encoding.UTF8.GetBytes(sb.ToString());

            _memoryCache.Set(cacheKey, bytes, TimeSpan.FromHours(24));
            return File(bytes, contentType);

        }
        [Route("/robots.txt")]
        public ContentResult RobotsTxt()
        {
            var sb = new StringBuilder();
            sb.AppendLine("User-agent: *")
                .AppendLine("Allow: /")
                .AppendLine("Disallow:")
                .AppendLine("Disallow: /cgi-bin/")
                .Append("sitemap: ")
                .Append(this.Request.Scheme)
                .Append("://")
                .Append(this.Request.Host)
                .AppendLine("/sitemap.xml");

            return this.Content(sb.ToString(), "text/plain", Encoding.UTF8);
        }

        [HttpPost]
        public async Task<IActionResult> GelenData([FromBody] Users data)
        {

            try
            {
                data.BirthDate = DateTime.Now;
                data.JobStartDate = DateTime.Now;

                // Veritabanına ekleme işlemi
                var result = await _userRepository.AddAsync(data);
                if (result.Success == false)
                {
                    return Json("Hata");
                }
                return Json("Basarili");
            }
            catch (Exception ex)
            {
                return BadRequest("Error deserializing data: " + ex.Message);
            }
        }
    }
}
