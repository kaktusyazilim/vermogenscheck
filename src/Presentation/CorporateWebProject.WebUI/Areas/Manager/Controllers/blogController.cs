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
    public class blogController : BaseController<Blogs>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IMemoryCache _memoryCache;

        public blogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IFileRepository fileRepository, IMemoryCache memoryCache) : base(blogRepository, AuthPage.Blog)
        {

            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _fileRepository = fileRepository;
            _memoryCache = memoryCache;
        }
        [Auth("Read", AuthPage.Blog)]
        public async Task<IActionResult> Index()
        {
            ServiceVM serviceVM = new ServiceVM(HttpContext, _memoryCache);
            await serviceVM.LoadLanguageSettingsAsync(HttpContext);
            serviceVM.BlogList = (await _blogRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            serviceVM.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false && x.LangId == serviceVM.CurrentLanguage.Id)).Data;
            return View(serviceVM);
        }
        [Auth("Create", AuthPage.Blog)]
        [HttpGet]
        public async Task<IActionResult> create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadModulAndPageDataAsync(HttpContext);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false && (x.TypeId == model.Type.Id) && x.LangId == model.CurrentLanguage.Id)).Data;

            return View(model);
        }

        [Auth("Create", AuthPage.Blog)]
        [HttpPost]
        public async Task<IActionResult> create(ServiceVM model, IFormCollection fc)
        {
            var currentItem = _blogRepository.Get(x => x.FriendlyUrl == model.Blog.FriendlyUrl).Result.Data;
            await model.LoadUserDataAsync(HttpContext);
            var currentFriendlyUrl = model.Blog.FriendlyUrl;
            if (fc.Files["files"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["files"]);
                model.Blog.Photo = imageResult.Path;
            }
            if (currentItem != null)
            {
                model.Blog.FriendlyUrl = model.Blog.FriendlyUrl + "-" + (new Random()).Next(10000, 99999).ToString();
            }
            else
            {
                model.Blog.FriendlyUrl =model.Blog.FriendlyUrl==""? FriendlyUrl.FriendlyURLTitle(model.Blog.Title):FriendlyUrl.FriendlyURLTitle(model.Blog.FriendlyUrl);
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
                model.Blog.Gallery = JsonConvert.SerializeObject(pictures);


            }


            model.Blog.CreatedUserId = model.CurrentUser.Id;
            model.Blog.Tags = model.Blog.Tags == null ? "" : model.Blog.Tags;
            model.Blog.MetaDescription = model.Blog.MetaDescription ?? "";
            model.Blog.ShortDescription = model.Blog.ShortDescription ?? "";
            model.Blog.FullDescription = model.Blog.FullDescription ?? "";
            model.Blog.Title = model.Blog.Title ?? "";
            var result = await _blogRepository.AddAsync(model.Blog);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/blog");
        }

        [Auth("Update", AuthPage.Blog)]
        [HttpGet]
        public async Task<IActionResult> update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false && x.TypeId == model.Type.Id)).Data;
            model.Blog = _blogRepository.Get(x => x.ItemGuid == id).Result.Data;
            return View(model);
        }

        [Auth("Update", AuthPage.Blog)]
        [HttpPost]
        public async Task<IActionResult> update(ServiceVM model, IFormCollection fc)
        {
            var currentitem = _blogRepository.Get(x => x.ItemGuid == model.Blog.ItemGuid).Result.Data;
            if (currentitem != null)
            {
                var currentFriendlyUrl = FriendlyUrl.FriendlyURLTitle(model.Blog.Title);
                currentitem.Title = model.Blog.Title;
                currentitem.FullDescription = model.Blog.FullDescription ?? "";
                currentitem.MetaDescription = model.Blog.MetaDescription ?? "";
                currentitem.ShortDescription = model.Blog.ShortDescription ?? "";
                currentitem.CategoryId = model.Blog.CategoryId;
                currentitem.IsPin = model.Blog.IsPin;
                currentitem.IsHomePage = model.Blog.IsHomePage;
                currentitem.LangId = model.Blog.LangId;
                currentitem.Tags = model.Blog.Tags ?? "";
                model.Blog.FriendlyUrl = FriendlyUrl.FriendlyURLTitle(model.Blog.Title);
                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    currentitem.Photo = imageResult.Path;
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
                    currentitem.Gallery = JsonConvert.SerializeObject(pictures);


                }


                var result = await _blogRepository.UpdateAsync(currentitem);

                base.SetResponseMessage(result.Success);
                return Redirect("/manager/blog");
            }

            base.SetResponseMessage(false);
            return Redirect("/manager/blog");
        }

    }
}
