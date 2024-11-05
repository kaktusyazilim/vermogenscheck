using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class accountsController : BaseController<Accounts>
    {
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly IAccountRepository _accountsRepository;
        private readonly IMemoryCache _memoryCache;
        public accountsController(IAccountTransactionRepository accountTransactionRepository, IAccountRepository accountsRepository, IMemoryCache memoryCache) : base(accountsRepository, AuthPage.Accounts)
        {
            _accountTransactionRepository = accountTransactionRepository;
            _accountsRepository = accountsRepository;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Auth("Read", AuthPage.Accounts)]
        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadUserDataAsync(HttpContext);
            await model.LoadModulAndPageDataAsync(HttpContext);
            model.AccountList = (await _accountsRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.AccountTransactionList = (await _accountTransactionRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Create", AuthPage.Accounts)]
        public IActionResult Get(string id)
        {
            var data = _accountsRepository.Get(x => x.ItemGuid == id).Result.Data;
            return Json(data);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Accounts)]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            await model.FillDataAsync(HttpContext);
            model.Account.CreateUserGuid = model.CurrentUser.ItemGuid;
            if (model.Account.ItemGuid != "")
            {
                var currentItem = _accountsRepository.Get(x => x.ItemGuid == model.Account.ItemGuid).Result.Data;
                if (fc["startAmount"].ToString() != "")
                {
                    var amount = Convert.ToDecimal(fc["startAmount"].ToString());
                }
                base.Equalize(currentItem, model.Account);
                var result = await _accountsRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
            }
            else
            {
                if (fc["startAmount"].ToString() != "")
                {
                    var amount = Convert.ToDecimal(fc["startAmount"].ToString());
                }
                var result = await _accountsRepository.AddAsync(model.Account);
                base.SetResponseMessage(result.Success);
            }

            return Redirect("/manager/accounts");
        }

        [HttpGet]
        [Auth("Update", AuthPage.Accounts)]
        public async Task<IActionResult> details(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            model.Account = _accountsRepository.Get(x => x.ItemGuid == id).Result.Data;
            model.AccountTransactionList = (await _accountTransactionRepository.GetListAsync(x => x.AccountGuid == id)).Data;
            return View(model);
        }

    }
}
