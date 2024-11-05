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
    public class categoryController : BaseController<Categories>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IModulRepository _modulRepository;
        private readonly IMemoryCache _memoryCache;
        public categoryController(ICategoryRepository categoryRepository, ITypeRepository typeRepository, IModulRepository modulRepository, IMemoryCache memoryCache) : base(categoryRepository, AuthPage.Category)
        {
            _categoryRepository = categoryRepository;
            _typeRepository = typeRepository;
            _modulRepository = modulRepository;
            _memoryCache = memoryCache;
        }
        [Auth("Read", AuthPage.Category)]
        public async Task<IActionResult> Index(string id = "")
        {
            // okeeeyyyy
            // tammalandı
            ServiceVM serviceVM = new ServiceVM(HttpContext, _memoryCache);
            serviceVM.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsDeleted == false)).Data.OrderBy(x => x.TypeName).ToList();
            if (id != "")
            {
                serviceVM.Category = (await _categoryRepository.Get(x => x.ItemGuid == id)).Data;
                serviceVM.CategoryList = serviceVM.CategoryList.Where(x => x.TypeId == serviceVM.Category.TypeId).ToList();

            }
            serviceVM.TypeList = (await _typeRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;

            return View(serviceVM);
        }
        [Auth("Create", AuthPage.Category)]
        [HttpGet]
        public async Task<IActionResult> create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.TypeList = (await _typeRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.Category)]
        [HttpPost]
        public IActionResult create(ServiceVM model, IFormCollection fc)
        {
            var type = _typeRepository.Get(x => x.Id == model.Category.TypeId).Result.Data;
            model.Category.FullDescription = model.Category.FullDescription ?? "";
            model.Category.MetaDescription = model.Category.MetaDescription ?? "";
            model.Category.ShortDescription = model.Category.ShortDescription ?? "";
            if (fc.Files["files"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["files"]);
                model.Category.Icon = imageResult.Path;
            }
            if (model.Category.IsLink != true)
            {
                model.Category.FriendlyUrl = FriendlyUrl.FriendlyURLTitle(model.Category.Name);
            }
            if (type != null)
                model.Category.TypeName = type.Name;

            List<string> pictures = new List<string>();
            if (fc.Files["pictures"] != null)
            {
                var fileList = fc.Files.Where(x => x.Name == "pictures").ToList();

                for (int i = 0; i < fileList.Count(); i++)
                {
                    var iiresult = base.CreateFile(fileList[i]);
                    pictures.Add(iiresult.Path);
                }
                model.Category.Gallery = JsonConvert.SerializeObject(pictures);


            }

            var result = _categoryRepository.AddAsync(model.Category);
            base.SetResponseMessage(result.Result.Success);
            return Redirect("/manager/category");
        }

        [Auth("Update", AuthPage.Category)]
        [HttpGet]
        public async Task<IActionResult> update(string id, int langid = 1)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.TypeList = (await _typeRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            model.Category = (await _categoryRepository.Get(x => x.LangId == model.CurrentLanguage.Id && x.ItemGuid == id)).Data;
            model.NewModul = (await _modulRepository.Get(x => x.CategoryId == model.Category.Id)).Data;
            return View(model);
        }

        [Auth("Update", AuthPage.Category)]
        [HttpPost]
        public async Task<IActionResult> update(ServiceVM model, IFormCollection fc)
        {
            await model.FillDataAsync(HttpContext);
            var currentItem = _categoryRepository.Get(x => x.ItemGuid == model.Category.ItemGuid && x.LangId == model.CurrentLanguage.Id).Result.Data;
            if (currentItem != null)
            {
                var type = _typeRepository.Get(x => x.Id == model.Category.TypeId).Result.Data;
                if (model.Category.IsLink != true)
                {
                    model.Category.FriendlyUrl = FriendlyUrl.FriendlyURLTitle(model.Category.Name);
                }
                if (type != null)
                    currentItem.TypeName = type.Name;
                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    currentItem.Icon = imageResult.Path;
                }
                if (model.Category.IsModul == true)
                {
                    var currentModulCategory = _modulRepository.Get(x => x.CategoryId == currentItem.Id).Result.Data;
                    if (currentModulCategory != null)
                    {
                        currentModulCategory.Title = model.NewModul.Title;
                        currentModulCategory.Queue = model.NewModul.Queue;

                    }
                    else
                    {
                        var newModul = new Modules()
                        {
                            IsShow = true,
                            LangId = currentItem.LangId,
                            Queue = model.NewModul.Queue,
                            Title = model.NewModul.Title,
                            Url = currentItem.ItemGuid,
                            CategoryId = currentItem.Id,
                            Icon = "",
                        };

                        await _modulRepository.AddAsync(newModul);
                    }

                }
                else
                {
                    var currentModulCategory = _modulRepository.Get(x => x.CategoryId == currentItem.Id).Result.Data;
                    if (currentModulCategory != null)
                    {
                        await _modulRepository.Delete(currentModulCategory);
                    }
                }
                currentItem.IsModul = model.Category.IsModul;
                currentItem.Name = model.Category.Name;
                currentItem.Queue = model.Category.Queue;
                currentItem.FullDescription = model.Category.FullDescription ?? "";
                currentItem.MetaDescription = model.Category.MetaDescription ?? "";
                currentItem.ShortDescription = model.Category.ShortDescription ?? "";
                currentItem.FriendlyUrl = model.Category.FriendlyUrl != null && model.Category.FriendlyUrl != "" ? model.Category.FriendlyUrl : FriendlyUrl.FriendlyURLTitle(model.Category.Name);
                currentItem.IsLink = model.Category.IsLink;
                currentItem.LangId = model.Category.LangId;
                currentItem.IsShow = model.Category.IsShow;
                currentItem.IsPage = model.Category.IsPage;
                currentItem.TypeId = model.Category.TypeId;
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


                var result = _categoryRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Result.Success);
                return Redirect("/manager/category");
            }
            return View();
        }


    }
}
