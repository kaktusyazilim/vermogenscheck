using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class commentsController : BaseController<Comments>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMemoryCache _memoryCache;

        public commentsController(ICommentRepository commentRepository, ISubCategoryRepository subCategoryRepository, IMemoryCache memoryCache) : base(commentRepository, AuthPage.SmtpSettings)
        {
            _commentRepository = commentRepository;
            _subCategoryRepository = subCategoryRepository;
            _memoryCache = memoryCache;
        }

        [Auth("Read", AuthPage.SmtpSettings)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.CommentList = (await _commentRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [Auth("Create", AuthPage.SmtpSettings)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            return View(model);
        }
        [Auth("Create", AuthPage.SmtpSettings)]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            if (fc.Files["pictures"] != null)
            {
                var imageResult = base.CreateFile(fc.Files["pictures"]);
                model.Comment.FilePath = imageResult.Path;
            }
            if (fc.Files["Video"] != null)
            {
                var imagePath = base.CreateFile(fc.Files["Video"]);
                model.Comment.VideoPath = imagePath.Path;
            }
            var result = await _commentRepository.AddAsync(model.Comment);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/comments");
            return View();
        }


        [Auth("Update", AuthPage.SmtpSettings)]
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.SubCategoryList = (await _subCategoryRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            model.Comment =(await _commentRepository.Get(x => x.ItemGuid == id)).Data;
            return View(model);
        }
        [Auth("Update", AuthPage.SmtpSettings)]
        [HttpPost]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem = (await _commentRepository.Get(x => x.ItemGuid == model.Comment.ItemGuid)).Data;
            if (currentItem != null)
            {
                if (fc.Files["pictures"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["pictures"]);
                    model.Comment.FilePath = imageResult.Path;
                }
                if (fc.Files["Video"] != null)
                {
                    var imagePath = base.CreateFile(fc.Files["Video"]);
                    model.Comment.VideoPath = imagePath.Path;
                }
                base.Equalize(currentItem, model.Comment);
                var result = await _commentRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/comments");
            }
            return View();
        }
    }
}
