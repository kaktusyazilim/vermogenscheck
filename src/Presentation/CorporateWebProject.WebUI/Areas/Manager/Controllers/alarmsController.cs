using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class alarmsController : BaseController<Alarms>

    {
        private readonly IAlarmRepository _alarmRepository;
        private readonly IMemoryCache _memoryCache;

        public alarmsController(IAlarmRepository alarmRepository, IMemoryCache memoryCache) : base(alarmRepository, AuthPage.Alarms)
        {
            _alarmRepository = alarmRepository;
            _memoryCache = memoryCache;
        }
        [Auth("Read", AuthPage.Alarms)]
        [HttpGet]
        public IActionResult Get(string id)
        {
            var data = _alarmRepository.Get(x => x.ItemGuid == id);
            return Json(data);
        }


        [Auth("Read", AuthPage.Alarms)]

        public async Task<IActionResult> GetList()
        {
            ServiceVM model = new ServiceVM();
            await model.FillDataAsync(HttpContext);
            var data = (await _alarmRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false && x.UserGuid == model.CurrentUser.ItemGuid));
            return Json(data);
        }
        [Auth("Create", AuthPage.Alarms)]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            await model.FillDataAsync(HttpContext);
            if (fc["clientid"].ToString() != "")
            {
                model.Alarms.ClientGuid = fc["clientid"].ToString();
            }
            model.Alarms.AlarmDate = new DateTime(model.Alarms.AlarmDate.Year, model.Alarms.AlarmDate.Month, model.Alarms.AlarmDate.Day, model.Alarms.AlarmTime.Hour, model.Alarms.AlarmTime.Minute, 0);
            model.Alarms.UserGuid = model.CurrentUser.ItemGuid;
            var result = await _alarmRepository.AddAsync(model.Alarms);
            base.SetResponseMessage(result.Success);
            return Json(result);
        }
        [Auth("Update", AuthPage.Alarms)]
        [HttpPost]
        public async Task<IActionResult> Update(ServiceVM model)
        {
            var result = await _alarmRepository.UpdateAsync(model.Alarms);
            base.SetResponseMessage(result.Success);
            return Json(result);
        }
    }
}
