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

    public class subcategoryController : BaseController<SubCategories>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMemoryCache _memoryCache;
        public subcategoryController(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IMemoryCache memoryCache) : base(subCategoryRepository, AuthPage.Subcategory)
        {
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _memoryCache = memoryCache;
        }
        [Auth("Read", AuthPage.Subcategory)]
        public async Task<IActionResult> Index(string id = "")
        {
            ServiceVM serviceVM = new ServiceVM(HttpContext, _memoryCache);
            serviceVM.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            serviceVM.CategoryList = (await _categoryRepository.GetListAsync()).Data;
            return View(serviceVM);
        }
        [Auth("Create", AuthPage.Subcategory)]
        [HttpGet]
        public async Task<IActionResult> create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.CategoryList = (await _categoryRepository.GetListAsync()).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.Subcategory)]
        [HttpPost]
        public async Task<IActionResult> create(ServiceVM model, IFormCollection fc)
        {
            var category = (await _categoryRepository.Get(x => x.Id == model.SubCategory.CategoryId)).Data;
            if (category != null)
            {
                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    model.SubCategory.Icon = imageResult.Path;
                }
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
                var result = await _subCategoryRepository.AddAsync(model.SubCategory);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/subcategory");
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
            model.SubCategory = (await _subCategoryRepository.Get(x => x.ItemGuid == id && x.LangId == model.CurrentLanguage.Id)).Data;
            return View(model);
        }

        [Auth("Update", AuthPage.Subcategory)]
        [HttpPost]
        public async Task<IActionResult> update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = (await _subCategoryRepository.Get(x => x.ItemGuid == model.SubCategory.ItemGuid && x.LangId == model.CurrentLanguage.Id)).Data;
            if (currentItem != null)
            {
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
                var result = await _subCategoryRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/subcategory");
            }
            base.SetResponseMessage(false);
            return Redirect("/manager/subcategory");
        }
        [Auth("Read", AuthPage.Subcategory)]
        public async Task<IActionResult> GetSubcategoryForCategory(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            var category = (await _categoryRepository.Get(x => x.LangId == model.CurrentSettings.LangId && x.ItemGuid == id)).Data;
            var models = (await _subCategoryRepository.GetListAsync(x => x.LangId == model.CurrentSettings.LangId && x.CategoryId == category.Id && x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x => x.Queue).ToList();
            return Json(models);
        }
    }
}
