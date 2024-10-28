using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.FriendlyUrl;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("manager")]
    public class subcategoryPagesController : BaseController<SmtpSettings>
    {
        private readonly ISmtpSettingRepository _smtpSettingRepository;
        private readonly IModulRepository _modulRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IMemoryCache _memoryCache;

        public subcategoryPagesController(ISmtpSettingRepository smtpSettingRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IModulRepository modullRepository, ITypeRepository typeRepository, IMemoryCache memoryCache) : base(smtpSettingRepository, AuthPage.SmtpSettings)
        {
            _smtpSettingRepository = smtpSettingRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _modulRepository = modullRepository;
            _typeRepository = typeRepository;
            _memoryCache = memoryCache;
        }

        [Auth("Read", AuthPage.SmtpSettings)]
        public async Task<IActionResult> Index(string id)
        {

            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Category = _categoryRepository.Get(x => x.ItemGuid == id).Result.Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.CategoryId == model.Category.Id && x.IsDeleted == false)).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.Subcategory)]
        [HttpGet]
        public IActionResult create(string categoryGuid)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Category = _categoryRepository.Get(x => x.ItemGuid == categoryGuid).Result.Data;
            model.Modul = _modulRepository.Get(x => x.CategoryId == model.Category.Id).Result.Data;

            return View(model);
        }

        [Auth("Create", AuthPage.Subcategory)]
        [HttpPost]
        public IActionResult create(ServiceVM model, IFormCollection fc)
        {
            var category = _categoryRepository.Get(x => x.Id == model.Category.Id).Result.Data;
            if (category != null)
            {
                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    model.SubCategory.Icon = imageResult.Path;
                }
                model.SubCategory.CategoryId = category.Id;
                model.SubCategory.FriendlyUrl = FriendlyUrl.FriendlyURLTitle(model.SubCategory.SubCategoryName);
                model.SubCategory.ShortDescription = model.SubCategory.ShortDescription ?? "";
                model.SubCategory.FullDescription = model.SubCategory.FullDescription ?? "";
                model.SubCategory.MetaDescription = model.SubCategory.MetaDescription ?? "";
                model.SubCategory.TypeId = category.TypeId;
                model.SubCategory.LangId = category.LangId;
                List<string> pictures = new List<string>();
                if (fc.Files["pictures"] != null)
                {
                    var fileList = fc.Files.Where(x => x.Name == "pictures").ToList();

                    for (int i = 0; i < fileList.Count(); i++)
                    {
                        var iiresult = base.CreateFile(fileList[i]);
                        pictures.Add(iiresult.Path);
                    }
                    model.SubCategory.Gallery = JsonConvert.SerializeObject(pictures);


                }
                var result = _subCategoryRepository.AddAsync(model.SubCategory);
                base.SetResponseMessage(result.Result.Success);
                return Redirect("/manager/subcategorypages?id=" + category.ItemGuid);
            }

            base.SetResponseMessage(false);
            return Redirect("/manager/subcategory");
        }

        [Auth("Update", AuthPage.Subcategory)]
        [HttpGet]
        public async Task<IActionResult> update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.CategoryList = (await _categoryRepository.GetListAsync()).Data;
            model.SubCategory = (await _subCategoryRepository.Get(x => x.ItemGuid == id)).Data;
            model.Modul = (await _modulRepository.Get(x => x.CategoryId == model.SubCategory.CategoryId)).Data;

            return View(model);
        }

        [Auth("Update", AuthPage.Subcategory)]
        [HttpPost]
        public IActionResult update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = _subCategoryRepository.Get(x => x.ItemGuid == model.SubCategory.ItemGuid).Result.Data;
            if (currentItem != null)
            {
                var category = _categoryRepository.Get(x => x.Id == currentItem.CategoryId).Result.Data;

                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    currentItem.Icon = imageResult.Path;
                }
                if (model.SubCategory.IsLink != true)
                {
                    currentItem.FriendlyUrl = FriendlyUrl.FriendlyURLTitle(model.SubCategory.SubCategoryName);
                }
                else
                {
                    currentItem.FriendlyUrl = model.SubCategory.FriendlyUrl;
                }
                currentItem.CategoryId = model.SubCategory.CategoryId;
                currentItem.SubCategoryName = model.SubCategory.SubCategoryName;
                currentItem.Queue = model.SubCategory.Queue;
                currentItem.IsModul = model.SubCategory.IsModul;
                currentItem.IsLink = model.SubCategory.IsLink;
                currentItem.ShortDescription = model.SubCategory.ShortDescription ?? "";
                currentItem.FullDescription = model.SubCategory.FullDescription ?? "";
                currentItem.MetaDescription = model.SubCategory.MetaDescription ?? "";
                List<string> pictures = new List<string>();
                if (fc.Files["pictures"] != null)
                {
                    var fileList = fc.Files.Where(x => x.Name == "pictures").ToList();

                    for (int i = 0; i < fileList.Count(); i++)
                    {
                        var iiresult = base.CreateFile(fileList[i]);
                        pictures.Add(iiresult.Path);
                    }
                    currentItem.Gallery = JsonConvert.SerializeObject(pictures);


                }
                var result = _subCategoryRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Result.Success);
                return Redirect("/manager/subcategorypages?id=" + category.ItemGuid);
            }
            base.SetResponseMessage(false);
            return Redirect("/manager/subcategory");
        }
        [Auth("Read", AuthPage.Subcategory)]
        public async Task<IActionResult> GetSubcategoryForCategory(string id)
        {
            var category = (await _categoryRepository.Get(x => x.ItemGuid == id)).Data;
            var model = (await _subCategoryRepository.GetListAsync(x => x.CategoryId == category.Id)).Data.OrderBy(x => x.Queue).ToList();
            return Json(model);
        }

    }
}
