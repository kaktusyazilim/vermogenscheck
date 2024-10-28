//using CorporateWebProject.Application.Repositories;
//using CorporateWebProject.WebUI.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Caching.Memory;

//namespace CorporateWebProject.WebUI.Controllers
//{
//    public class pagesController : Controller
//    {
//        private readonly ITypeRepository _typeRepository;

//        private readonly ICategoryRepository _categoryRepository;
//        private readonly ISubCategoryRepository _subCategoryRepository;
//        private readonly ILowestCategoryRepository _lowestCategoryRepository;
//        private readonly IHistoryRepository _historyRepository;
//        private readonly IMemoryCache _memoryCache;
//        public pagesController(ITypeRepository typeRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, ILowestCategoryRepository lowestCategoryRepository, IHistoryRepository historyRepository, IMemoryCache memoryCache)
//        {
//            _typeRepository = typeRepository;
//            _categoryRepository = categoryRepository;
//            _subCategoryRepository = subCategoryRepository;
//            _lowestCategoryRepository = lowestCategoryRepository;
//            _historyRepository = historyRepository;
//            _memoryCache = memoryCache;
//        }
//        [Route("sayfa/{type}/{id}")]
//        public async Task<IActionResult> Index(string type,string id)
//        {
//            ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
//            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
//            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
//            if(type=="category")
//            model.Category = _categoryRepository.Get(x => x.FriendlyUrl == type).Result.Data;
//            model.SubCategory = _subCategoryRepository.Get(x => x.FriendlyUrl == id).Result.Data;
//            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.CategoryId == model.SubCategory.CategoryId && x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x => x.Queue).ToList();
//            if (type == "subcategory")
//            {
//                model.SubCategory=_subCategoryRepository.Get(x=>x.FriendlyUrl== id).Result.Data;
//                model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.CategoryId == model.SubCategory.CategoryId && x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x=>x.Queue).ToList();

//            }
//            if (type == "services")
//            {

//            }
//            if (type == "lowestcategory")
//            {
//                model.LowestCategory=_lowestCategoryRepository.Get(x=>x.FriendlyUrl==id).Result.Data;
//            }

//                return View(model);
//        }


//        [Route("tarihce")]
//        public async IActionResult history()
//        {
//            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
//            model.HistoryList = (await _historyRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data.OrderBy(x=>x.Queue).ToList();
//            model.CategoryList = _categoryRepository.GetList(x => x.IsPassive == false && x.IsDeleted == false).Data;
//            model.SubCategoryList = _subCategoryRepository.GetList(x => x.IsPassive == false && x.IsDeleted == false).Data;
//            return View(model);
//        }
//    }
//}
