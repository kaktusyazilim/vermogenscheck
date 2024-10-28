using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class notesController : BaseController<Notes>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMemoryCache _memoryCache;
        public notesController(INoteRepository noteRepository, IMemoryCache memoryCache) : base(noteRepository, AuthPage.Notes)
        {
            _noteRepository = noteRepository;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Auth("Read", AuthPage.Notes)]

        public IActionResult Get(string id)
        {
            var data = _noteRepository.Get(x => x.ItemGuid == id).Result;
            return Json(data);
        }
        [HttpGet]
        [Auth("Read", AuthPage.Notes)]
        public async Task<IActionResult> GetList()
        {
            ServiceVM model = new ServiceVM();
               await model.FillDataAsync(HttpContext);
            var data = (await _noteRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false && x.UserGuid == model.CurrentUser.ItemGuid));
            return Json(data.Data.OrderByDescending(x=>x.CreateDate).ToList());
        }
        [HttpPost]
        [Auth("Create", AuthPage.Notes)]
        public async Task<IActionResult> Create(ServiceVM model,IFormCollection fc)
        {
               await model.FillDataAsync(HttpContext);
            model.Note.UserGuid = model.CurrentUser.ItemGuid;
            if (fc["noteid"].ToString()!="")
            {
                var currentItem=  _noteRepository.Get(x => x.ItemGuid == fc["noteid"].ToString()).Result.Data;
                currentItem.Title=model.Note.Title;
                currentItem.Description= model.Note.Description;
                var result = await _noteRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Json(result);
            }
            else
            {
                var result = await _noteRepository.AddAsync(model.Note);
                base.SetResponseMessage(result.Success);
                return Json(result);
            }
          
        }
        [HttpPost]
        [Auth("Update", AuthPage.Notes)]
        public async Task<IActionResult> Update(ServiceVM model)
        {
            var result = await _noteRepository.UpdateAsync(model.Note);
            base.SetResponseMessage(result.Success);
            return Json(result);
        }
    }
}
