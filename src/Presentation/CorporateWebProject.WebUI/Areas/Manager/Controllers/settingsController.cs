using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class settingsController : BaseController<Settings>
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMemoryCache _memoryCache;

        public settingsController(ISettingRepository settingRepository, IMemoryCache memoryCache) : base(settingRepository, AuthPage.Settings)
        {
            _settingRepository = settingRepository;
            _memoryCache = memoryCache;
        }
        [Auth("Read", AuthPage.Settings)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.SettingList = (await _settingRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }
        [Auth("Read", AuthPage.Settings)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            return View(model);
        }
        [Auth("Update", AuthPage.Settings)]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            model.Setting.Title = model.Setting.Title == null ? "" : model.Setting.Title;
            model.Setting.Url = model.Setting.Url == null ? "" : model.Setting.Url;
            model.Setting.AboutTitle = model.Setting.AboutTitle == null ? "" : model.Setting.AboutTitle;
            model.Setting.FooterScripts = model.Setting.FooterScripts == null ? "" : model.Setting.FooterScripts;
            model.Setting.HeaderScripts = model.Setting.HeaderScripts == null ? "" : model.Setting.HeaderScripts;
            model.Setting.AboutDescription = model.Setting.AboutDescription == null ? "" : model.Setting.AboutDescription;
            model.Setting.AboutPhoto = model.Setting.AboutPhoto == null ? "" : model.Setting.AboutPhoto;
            model.Setting.AboutTitle = model.Setting.AboutTitle == null ? "" : model.Setting.AboutTitle;
            model.Setting.AboutVideoLink = model.Setting.AboutVideoLink == null ? "" : model.Setting.AboutVideoLink;
            model.Setting.Address = model.Setting.Address == null ? "" : model.Setting.Address;
            model.Setting.BodyScripts = model.Setting.BodyScripts == null ? "" : model.Setting.BodyScripts;
            model.Setting.BreadcrumbImage = model.Setting.BreadcrumbImage == null ? "" : model.Setting.BreadcrumbImage;
            model.Setting.DarkLogo = model.Setting.DarkLogo == null ? "" : model.Setting.DarkLogo;
            model.Setting.FacebookDomainVeritification = model.Setting.FacebookDomainVeritification == null ? "" : model.Setting.FacebookDomainVeritification;
            model.Setting.FacebookId = model.Setting.FacebookId == null ? "" : model.Setting.FacebookId;
            model.Setting.Favicon = model.Setting.Favicon == null ? "" : model.Setting.Favicon;
            model.Setting.FooterScripts = model.Setting.FooterScripts == null ? "" : model.Setting.FooterScripts;
            model.Setting.GoogleMap = model.Setting.GoogleMap == null ? "" : model.Setting.GoogleMap;
            model.Setting.GoogleSiteVeritification = model.Setting.GoogleSiteVeritification == null ? "" : model.Setting.GoogleSiteVeritification;
            model.Setting.HeaderScripts = model.Setting.HeaderScripts == null ? "" : model.Setting.HeaderScripts;
            model.Setting.Mail = model.Setting.Mail == null ? "" : model.Setting.Mail;
            model.Setting.MetaDescription = model.Setting.MetaDescription == null ? "" : model.Setting.MetaDescription;
            model.Setting.Mission = model.Setting.Mission == null ? "" : model.Setting.Mission;
            model.Setting.MissionPhoto = model.Setting.MissionPhoto == null ? "" : model.Setting.MissionPhoto;
            model.Setting.Phone1 = model.Setting.Phone1 == null ? "" : model.Setting.Phone1;
            model.Setting.Phone2 = model.Setting.Phone2 == null ? "" : model.Setting.Phone2;
            model.Setting.SeperatorImage = model.Setting.SeperatorImage == null ? "" : model.Setting.SeperatorImage;
            model.Setting.SiteName = model.Setting.SiteName == null ? "" : model.Setting.SiteName;
            model.Setting.TwitterId = model.Setting.TwitterId == null ? "" : model.Setting.TwitterId;
            model.Setting.Url = model.Setting.Url == null ? "" : model.Setting.Url;
            model.Setting.Vision = model.Setting.Vision == null ? "" : model.Setting.Vision;
            model.Setting.VisionPhoto = model.Setting.VisionPhoto == null ? "" : model.Setting.VisionPhoto;
            model.Setting.WebDescription = model.Setting.WebDescription == null ? "" : model.Setting.WebDescription;
            model.Setting.WhiteLogo = model.Setting.WhiteLogo == null ? "" : model.Setting.WhiteLogo;
            model.Setting.WorkHours = model.Setting.WorkHours == null ? "" : model.Setting.WorkHours;
            model.Setting.YandexVeritification = model.Setting.YandexVeritification == null ? "" : model.Setting.YandexVeritification;

            if (fc.Files["DarkLogo"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["DarkLogo"]);
                model.Setting.DarkLogo = imageResult.Path;
            }
            if (fc.Files["WhiteLogo"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["WhiteLogo"]);
                model.Setting.WhiteLogo = imageResult.Path;
            }
            if (fc.Files["Favicon"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["Favicon"]);
                model.Setting.Favicon = imageResult.Path;
            }
            if (fc.Files["BreadcrumbPhoto"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["BreadcrumbPhoto"]);
                model.Setting.BreadcrumbImage = imageResult.Path;
            }
            if (fc.Files["SeperatorPhoto"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["SeperatorPhoto"]);
                model.Setting.SeperatorImage = imageResult.Path;
            }

            var result = await _settingRepository.AddAsync(model.Setting);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/settings");




            return View();
        }

        [Auth("Read", AuthPage.Settings)]
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.Setting = _settingRepository.Get(x => x.ItemGuid == id).Result.Data;
            return View(model);
        }
        [Auth("Update", AuthPage.Settings)]
        [HttpPost]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            if (model.Setting.ItemGuid != null && model.Setting.ItemGuid != "")
            {
                var currentItem = _settingRepository.Get(x => x.ItemGuid == model.Setting.ItemGuid).Result.Data;
                currentItem.Title = model.Setting.Title == null ? "" : model.Setting.Title;
                currentItem.Url = model.Setting.Url == null ? "" : model.Setting.Url;
                currentItem.Faks = model.Setting.Faks == null ? "" : model.Setting.Faks;
                currentItem.AboutTitle = model.Setting.AboutTitle == null ? "" : model.Setting.AboutTitle;
                currentItem.FooterScripts = model.Setting.FooterScripts == null ? "" : model.Setting.FooterScripts;
                currentItem.HeaderScripts = model.Setting.HeaderScripts == null ? "" : model.Setting.HeaderScripts;
                currentItem.AboutDescription = model.Setting.AboutDescription == null ? "" : model.Setting.AboutDescription;
                currentItem.AboutPhoto = model.Setting.AboutPhoto == null ? "" : model.Setting.AboutPhoto;
                currentItem.AboutTitle = model.Setting.AboutTitle == null ? "" : model.Setting.AboutTitle;
                currentItem.AboutVideoLink = model.Setting.AboutVideoLink == null ? "" : model.Setting.AboutVideoLink;
                currentItem.Address = model.Setting.Address == null ? "" : model.Setting.Address;
                currentItem.BodyScripts = model.Setting.BodyScripts == null ? "" : model.Setting.BodyScripts;
                currentItem.FacebookDomainVeritification = model.Setting.FacebookDomainVeritification == null ? "" : model.Setting.FacebookDomainVeritification;
                currentItem.FacebookId = model.Setting.FacebookId == null ? "" : model.Setting.FacebookId;
                currentItem.FooterScripts = model.Setting.FooterScripts == null ? "" : model.Setting.FooterScripts;
                currentItem.GoogleMap = model.Setting.GoogleMap == null ? "" : model.Setting.GoogleMap;
                currentItem.GoogleSiteVeritification = model.Setting.GoogleSiteVeritification == null ? "" : model.Setting.GoogleSiteVeritification;
                currentItem.HeaderScripts = model.Setting.HeaderScripts == null ? "" : model.Setting.HeaderScripts;
                currentItem.Mail = model.Setting.Mail == null ? "" : model.Setting.Mail;
                currentItem.MetaDescription = model.Setting.MetaDescription == null ? "" : model.Setting.MetaDescription;
                currentItem.Mission = model.Setting.Mission == null ? "" : model.Setting.Mission;
                currentItem.MissionPhoto = model.Setting.MissionPhoto == null ? "" : model.Setting.MissionPhoto;
                currentItem.Phone1 = model.Setting.Phone1 == null ? "" : model.Setting.Phone1;
                currentItem.Phone2 = model.Setting.Phone2 == null ? "" : model.Setting.Phone2;
                currentItem.SiteName = model.Setting.SiteName == null ? "" : model.Setting.SiteName;
                currentItem.TwitterId = model.Setting.TwitterId == null ? "" : model.Setting.TwitterId;
                currentItem.Url = model.Setting.Url == null ? "" : model.Setting.Url;
                currentItem.Vision = model.Setting.Vision == null ? "" : model.Setting.Vision;
                currentItem.VisionPhoto = model.Setting.VisionPhoto == null ? "" : model.Setting.VisionPhoto;
                currentItem.WebDescription = model.Setting.WebDescription == null ? "" : model.Setting.WebDescription;
                currentItem.WorkHours = model.Setting.WorkHours == null ? "" : model.Setting.WorkHours;
                currentItem.YandexVeritification = model.Setting.YandexVeritification == null ? "" : model.Setting.YandexVeritification;
                if (fc.Files["companyExcel"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["companyExcel"]);
                    currentItem.CompanyExcel = imageResult.Path;
                }
                if (fc.Files["DarkLogo"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["DarkLogo"]);
                    currentItem.DarkLogo = imageResult.Path;
                }
                if (fc.Files["WhiteLogo"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["WhiteLogo"]);
                    currentItem.WhiteLogo = imageResult.Path;
                }
                if (fc.Files["Favicon"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["Favicon"]);
                    currentItem.Favicon = imageResult.Path;
                }
                if (fc.Files["BreadcrumbPhoto"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["BreadcrumbPhoto"]);
                    currentItem.BreadcrumbImage = imageResult.Path;
                }
                if (fc.Files["SeperatorPhoto"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["SeperatorPhoto"]);
                    currentItem.SeperatorImage = imageResult.Path;
                }
                var result = await _settingRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/settings");
            }




            return View();
        }
    }
}
