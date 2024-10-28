using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class AccountTransactionsController : BaseController<AccountTransactions>
    {

        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly IAccountRepository _accountsRepository;
        private readonly IMemoryCache _memoryCache;

        public AccountTransactionsController(IAccountTransactionRepository accountTransactionRepository, IAccountRepository accountsRepository, IMemoryCache memoryCache) : base(accountTransactionRepository, AuthPage.AccountTransactions)
        {
            _accountTransactionRepository = accountTransactionRepository;
            _accountsRepository = accountsRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.AccountTransactions)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Auth("Create", AuthPage.AccountTransactions)]
        public IActionResult Get(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            var data = _accountTransactionRepository.Get(x => x.ItemGuid == id).Result.Data;
            return Json(data);
        }

        [HttpPost]
        [Auth("Create", AuthPage.AccountTransactions)]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {

            await model.FillDataAsync(HttpContext);
            model.AccountTransaction.CreateUserGuid = model.CurrentUser.ItemGuid;
            if (fc.Files["files"] != null)
            {
                var files = fc.Files.Where(x => x.Name == "files").ToList();
                List<string> fileList = new List<string>();
                foreach (var item in files)
                {
                    var imageResult = base.CreateFile(item);
                    fileList.Add(imageResult.Path);
                }
                model.AccountTransaction.Files = JsonConvert.SerializeObject(fileList);
            }
            model.AccountTransaction.TransactionDate = new DateTime(model.AccountTransaction.TransactionDate.Year, model.AccountTransaction.TransactionDate.Month, model.AccountTransaction.TransactionDate.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            model.AccountTransaction.AccountGuid = model.Account.ItemGuid;
            model.AccountTransaction.Amount = model.AccountTransaction.TransactionType == "GİRDİ" ? model.AccountTransaction.Amount : model.AccountTransaction.Amount * -1;

            if (model.AccountTransaction.ItemGuid != "")
            {
                var currentItem = _accountTransactionRepository.Get(x => x.ItemGuid == model.AccountTransaction.ItemGuid).Result.Data;
                model.AccountTransaction.TransactionDate = new DateTime(model.AccountTransaction.TransactionDate.Year, model.AccountTransaction.TransactionDate.Month, model.AccountTransaction.TransactionDate.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
                base.Equalize(currentItem, model.AccountTransaction);
                var updateResult = await _accountTransactionRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(updateResult.Success);
                return Redirect("/manager/accounts/details?id=" + model.Account.ItemGuid);

            }
            else
            {
                var result = await _accountTransactionRepository.AddAsync(model.AccountTransaction);
                base.SetResponseMessage(result.Success);

            }


            return Redirect("/manager/accounts/details?id=" + model.Account.ItemGuid);
            return View(model);
        }



        [HttpGet]
        [Auth("Update", AuthPage.AccountTransactions)]
        public IActionResult Update(string id)
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.AccountTransactions)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            return View(model);
        }

    }
}
