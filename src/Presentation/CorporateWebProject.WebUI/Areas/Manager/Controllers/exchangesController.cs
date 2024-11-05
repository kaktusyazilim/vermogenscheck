using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class exchangesController : BaseController<Exchanges>
    {

        private readonly IExchangeRepository _exchangeRepository;
        private readonly IMemoryCache _memoryCache;

        public exchangesController(IExchangeRepository exchangeRepository, IMemoryCache memoryCache) : base(exchangeRepository, AuthPage.Exchanges)
        {
            _exchangeRepository = exchangeRepository;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Auth("Read", AuthPage.Exchanges)]
        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.LoadLanguageSettingsAsync(HttpContext);
            model.ExchangesList = (await _exchangeRepository.GetListAsync()).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Read", AuthPage.Exchanges)]

        public IActionResult Create()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.Exchanges)]

        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            try
            {
                if (fc.Files["Icon"] != null)
                {
                    var image = base.CreateFile(fc.Files["Icon"]);
                    model.Exchanges.Icon = image.Path;
                }
                model.Exchanges.Name = model.Exchanges.Name ?? "";
                model.Exchanges.Description = model.Exchanges.Description ?? "";
                model.Exchanges.Count = Decimal.Parse(fc["Count"].ToString());


                var result = await _exchangeRepository.AddAsync(model.Exchanges);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/Exchanges");
            }
            catch (Exception)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/Exchanges");

            }
        }

        [HttpGet]
        [Auth("Read", AuthPage.Exchanges)]
        public async Task<IActionResult> Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Exchanges = (await _exchangeRepository.Get(x => x.ItemGuid == id)).Data;
            return View(model);
        }


        [HttpPost]
        [Auth("Read", AuthPage.Exchanges)]

        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            try
            {
                var currentModel = (await _exchangeRepository.Get(x => x.ItemGuid == model.Exchanges.ItemGuid)).Data;
                if (currentModel != null)
                {
                    if (fc.Files["Icon"] != null)
                    {
                        var image = base.CreateFile(fc.Files["Icon"]);
                        currentModel.Icon = image.Path;
                    }
                    currentModel.Name = model.Exchanges.Name ?? currentModel.Name;
                    //currentModel.Description = model.Exchanges.Description ?? currentModel.Description;

                    List<Decimal> oldCounts = new List<Decimal>();
                    if (currentModel.OldCounts != "")
                    {
                        oldCounts = JsonSerializer.Deserialize<List<decimal>>(currentModel.OldCounts);
                    }
                    else { 
                        oldCounts.Add(currentModel.Count);
                    }

                    if (fc["Count"].ToString() != null || fc["Count"].ToString() != "")
                    {
                        oldCounts.Add((Decimal)Decimal.Parse(fc["Count"].ToString()));
                    }
                    currentModel.OldCounts = JsonSerializer.Serialize(oldCounts);




                    currentModel.Count =  fc["Count"].ToString() != null ? Decimal.Parse(fc["Count"].ToString()) : currentModel.Count;



                    base.Equalize(model, currentModel);

                    var result = await _exchangeRepository.UpdateAsync(currentModel);
                    base.SetResponseMessage(result.Success);
                    return Redirect("/manager/Exchanges");


                }
                base.SetResponseMessage(false);
                return Redirect("/manager/Exchanges");
            }
            catch (Exception ex)
            {
                base.SetResponseMessage(false);
                return Redirect("/manager/Exchanges");

            }
        }
    }
}
