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
    public class categoryPagesController : BaseController<SmtpSettings>
    {
        private readonly ISmtpSettingRepository _smtpSettingRepository;
        private readonly IModulRepository _modulRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IMemoryCache _memoryCache;

        public categoryPagesController(ISmtpSettingRepository smtpSettingRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IModulRepository modullRepository, ITypeRepository typeRepository, IMemoryCache memoryCache) : base(smtpSettingRepository, AuthPage.SmtpSettings)
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
            
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.Type = _typeRepository.Get(x => x.ItemGuid == id).Result.Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsDeleted == false)).Data;

            if (id != "")
            {
                model.CategoryList = model.CategoryList.Where(x => x.TypeId == model.Type.Id).ToList();

            }
            model.SmtpSettingList = (await _smtpSettingRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

       
        [Auth("Create", AuthPage.Category)]
        [HttpGet]
        public async Task<IActionResult> create(string typeid)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.Type = (await _typeRepository.Get(x => x.ItemGuid == typeid)).Data;

            model.TypeList = (await _typeRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.Category)]
        [HttpPost]
        public async Task<IActionResult> create(ServiceVM model, IFormCollection fc)
        {
            var type = _typeRepository.Get(x => x.Id == model.Type.Id).Result.Data;
            model.Category.FullDescription = model.Category.FullDescription ?? "";
            model.Category.MetaDescription = model.Category.MetaDescription ?? "";
            model.Category.ShortDescription = model.Category.ShortDescription ?? "";
            model.Category.TypeId = model.Type.Id;
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

            var result = await _categoryRepository.AddAsync(model.Category);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/categoryPages?id="+type.ItemGuid);
        }

        [Auth("Update", AuthPage.Category)]
        [HttpGet]
        public async Task<IActionResult> update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.TypeList =(await _typeRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            model.Category = _categoryRepository.Get(x => x.ItemGuid == id).Result.Data;
            model.NewModul = _modulRepository.Get(x => x.CategoryId == model.Category.Id).Result.Data;
            return View(model);
        }

        [Auth("Update", AuthPage.Category)]
        [HttpPost]
        public async Task<IActionResult> update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = (await _categoryRepository.Get(x => x.ItemGuid == model.Category.ItemGuid)).Data;

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


                var result =await _categoryRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/categoryPages?id=" + type.ItemGuid);
            }
            return View();
        }

    }
}
