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

    public class lowestCategoryController : BaseController<LowestCategory>
    {
        private readonly ILowestCategoryRepository _lowestCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMemoryCache _memoryCache;
        public lowestCategoryController(ILowestCategoryRepository lowestCategoryRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IMemoryCache memoryCache) : base(lowestCategoryRepository, AuthPage.LowestCategory)
        {
            _lowestCategoryRepository = lowestCategoryRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.LowestCategory)]
        public async Task<IActionResult> Index(string id = "")
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.LowestCategoryList = (await _lowestCategoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.LowestCategory)]
        public async Task<IActionResult> Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.LowestCategory)]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            var category = (await _categoryRepository.Get(x => x.ItemGuid == model.LowestCategory.CategoryGuid)).Data;
            if (category != null)
            {
                var subcategory = (await _subCategoryRepository.Get(x => x.ItemGuid == model.LowestCategory.SubcategoryGuid)).Data;
                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    model.LowestCategory.Icon = imageResult.Path;
                }
                if (model.LowestCategory.IsLink != true)
                {
                    model.LowestCategory.FriendlyUrl = FriendlyUrl.FriendlyURLTitle(model.LowestCategory.Name);

                }
                model.LowestCategory.CategoryName = category.Name;
                model.LowestCategory.CategoryGuid = category.ItemGuid;
                model.LowestCategory.SubcategoryGuid = subcategory.ItemGuid;
                model.LowestCategory.SubCategoryName = subcategory.SubCategoryName;
                model.LowestCategory.CategoryId = category.Id;
                model.LowestCategory.SubcategoryId = subcategory.Id;
                model.LowestCategory.ShortDescription = model.LowestCategory.ShortDescription ?? "";
                model.LowestCategory.FullDescription = model.LowestCategory.FullDescription ?? "";
                model.LowestCategory.MetaDescription = model.LowestCategory.MetaDescription ?? "";
                model.LowestCategory.TypeId = category.TypeId;
                model.LowestCategory.LangId = category.LangId;
                List<string> pictures = new List<string>();
                if (fc.Files["pictures"] != null)
                {
                    var fileList = fc.Files.Where(x => x.Name == "pictures").ToList();

                    for (int i = 0; i < fileList.Count(); i++)
                    {
                        var iiresult = base.CreateFile(fileList[i]);
                        pictures.Add(iiresult.Path);
                    }
                    model.LowestCategory.Gallery = JsonConvert.SerializeObject(pictures);


                }
                var result = _lowestCategoryRepository.AddAsync(model.LowestCategory);
                base.SetResponseMessage(result.Result.Success);
                return Redirect("/manager/lowestcategory");
            }

            base.SetResponseMessage(false);
            return Redirect("/manager/lowestcategory");
            return View(model);
        }

        [HttpGet]
        [Auth("Update", AuthPage.LowestCategory)]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.LowestCategory = (await _lowestCategoryRepository.Get(x => x.ItemGuid == id)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.LowestCategory)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = _lowestCategoryRepository.Get(x => x.ItemGuid == model.LowestCategory.ItemGuid).Result.Data;
            if (currentItem != null)
            {
                var subcategory = _subCategoryRepository.Get(x => x.ItemGuid == model.LowestCategory.SubcategoryGuid).Result.Data;
                var category = _categoryRepository.Get(x => x.ItemGuid == model.LowestCategory.CategoryGuid).Result.Data;

                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    currentItem.Icon = imageResult.Path;
                }
                currentItem.SubCategoryName = subcategory.SubCategoryName;
                currentItem.SubcategoryGuid = subcategory.ItemGuid;
                currentItem.SubcategoryId = subcategory.Id;
                currentItem.CategoryName = category.Name;
                currentItem.CategoryGuid = category.ItemGuid;
                currentItem.CategoryId = category.Id;
                currentItem.IsModul = model.LowestCategory.IsModul;
                currentItem.Queue = model.LowestCategory.Queue;
                if (model.LowestCategory.IsLink == true)
                {
                    currentItem.FriendlyUrl = model.LowestCategory.FriendlyUrl;

                }
                else
                {
                    currentItem.FriendlyUrl = FriendlyUrl.FriendlyURLTitle(model.LowestCategory.Name);

                }

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
                currentItem.IsLink = model.LowestCategory.IsLink;
                currentItem.ShortDescription = model.LowestCategory.ShortDescription ?? "";
                currentItem.FullDescription = model.LowestCategory.FullDescription ?? "";
                currentItem.MetaDescription = model.LowestCategory.MetaDescription ?? "";
                var result = await _lowestCategoryRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/lowestCategory");
            }
            base.SetResponseMessage(false);
            return Redirect("/manager/lowestCategory");
        }


        [Auth("Read", AuthPage.LowestCategory)]
        public async Task<IActionResult> GetLowestCategoryForCategory(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);

            var subcategory = _subCategoryRepository.Get(x => x.LangId == model.CurrentSettings.LangId && x.ItemGuid == id).Result.Data;
            var models = (await _lowestCategoryRepository.GetListAsync(x => x.LangId == model.CurrentSettings.LangId && x.SubcategoryId == subcategory.Id && x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x => x.Queue).ToList();
            return Json(models);
        }

        [Auth("Read", AuthPage.LowestCategory)]
        public async Task<IActionResult> GetLowestCategoryForCategoryId(int id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            var models = (await _lowestCategoryRepository.GetListAsync(x => x.LangId == model.CurrentSettings.LangId && x.CategoryId == 92 && x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x => x.Queue).ToList();
            return Json(models);
        }
    }
}


