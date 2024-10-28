using CorporateWebProject.Application.Dto.Payment;
using CorporateWebProject.Application.Enums;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Models;
using CP.VPOS;
using CP.VPOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("manager")]
    public class paymentsController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IInvoiceStatuRepository _invoiceStatuRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMemoryCache _memoryCache;

        public paymentsController(IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, IInvoiceStatuRepository invoiceStatuRepository, ICompanyRepository companyRepository, IPaymentRepository paymentRepository, IMemoryCache memoryCache)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceDetailRepository = invoiceDetailRepository;
            _invoiceStatuRepository = invoiceStatuRepository;
            _companyRepository = companyRepository;
            _paymentRepository = paymentRepository;
            _memoryCache = memoryCache;
        }


        public async Task<IActionResult> Index(string invoiceNo)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.Invoice = (await _invoiceRepository.Get(x => x.InvoiceNumber == invoiceNo)).Data;
            model.InvoiceDetailList = (await _invoiceDetailRepository.GetListAsync(x => x.InvoiceGuid == model.Invoice.ItemGuid)).Data;
            model.InvoiceStatu = (await _invoiceStatuRepository.Get(x => x.Id == model.Invoice.InvoiceStatuId)).Data;
            model.InvoiceStatuList = (await _invoiceStatuRepository.GetListAsync()).Data;
            return View(model);
        }

        public VirtualPOSAuth _nestpayHalkBank = new VirtualPOSAuth
        {
            bankCode = CP.VPOS.Services.BankService.Halkbank,
            merchantID = "500200000",
            merchantUser = "api",
            merchantPassword = "TEST1111",
            merchantStorekey = "123456",
            testPlatform = true
        };
        [HttpPost]
        public async Task<IActionResult> startThreeD(ServiceVM model, IFormCollection fc)
        {
               await model.FillDataAsync(HttpContext);
            model.Invoice.ReturnId = Convert.ToInt32((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds).ToString("X");

            if (model.Invoice.InvoiceNumber != null)
            {
                model.Invoice = (await _invoiceRepository.Get(x => x.InvoiceNumber == model.Invoice.InvoiceNumber)).Data;
                model.InvoiceDetailList = (await _invoiceDetailRepository.GetListAsync(x => x.InvoiceGuid == model.Invoice.ItemGuid)).Data;
                model.InvoiceStatu = (await _invoiceStatuRepository.Get(x => x.Id == model.Invoice.InvoiceStatuId)).Data;
                model.Company = (await _companyRepository.Get(x => x.Code == model.Invoice.CustomerCode)).Data;
            }
            else
            {
                var asdad = Convert.ToDecimal(fc["Invoice.TotalPrice"].ToString().Replace("₺ ", ""));
                model.InvoiceStatu = _invoiceStatuRepository.Get(x => x.Id == 1).Result.Data;
                model.Company = _companyRepository.Get(x => x.Code == model.CurrentCompany.Code).Result.Data;

                Payments pay = new Payments()
                {
                    Amount = 0,
                    CardBrand = "",
                    CardIssuer = "",
                    ClientIp = "",
                    CompanyGuid = model.CurrentCompany.ItemGuid,
                    Installment = 0,
                    InvoiceId = "",
                    MaskedCreditCard = "",
                    MerchantName = "",
                    Response = "",
                    ReturnId = model.Invoice.ReturnId,
                    Xid = "",

                };

                var re = await _paymentRepository.AddAsync(pay);

            }

            CustomerInfo customerInfo = new CustomerInfo
            {
                taxNumber = "1111111111",
                emailAddress = model.Company.Mail,
                name = model.Company.AuthorizedName,
                surname = model.Company.AuthorizedSurname,
                phoneNumber = model.Company.Phone,
                addressDesc = model.Company.Address,
                cityName = "istanbul",
                country = CP.VPOS.Enums.Country.TUR,
                postCode = "34000",
                taxOffice = "maltepe",
                townName = "maltepe"
            };

            SaleRequest saleRequest = new SaleRequest
            {


                invoiceInfo = customerInfo,
                shippingInfo = customerInfo,
                saleInfo = new SaleInfo
                {
                    cardNameSurname = model.CreditCard.CardHolder,
                    cardNumber = model.CreditCard.CardNumber.Replace(" ", ""),
                    cardExpiryDateMonth = model.CreditCard.Month,
                    cardExpiryDateYear = model.CreditCard.Year,
                    amount = model.Invoice.TotalPrice == 0 ? Convert.ToDecimal(fc["Invoice.TotalPrice"].ToString().Replace("₺ ", "")) : (decimal)model.Invoice.TotalPrice,
                    cardCVV = model.CreditCard.CVC,
                    currency = CP.VPOS.Enums.Currency.TRY,
                    installment = 1,
                },
                payment3D = new Payment3D
                {
                    //returnURL = "https://localhost:5001/manager/payments/CallBack",
                    returnURL = "https://begos.kaktusyazilim.com/client/payments/CallBack",

                    confirm = true
                },
                customerIPAddress = "1.1.1.1",

                orderNumber = model.Invoice.ReturnId,
            };
            if (model.Invoice.InvoiceNumber != null)
                await _invoiceRepository.UpdateAsync(model.Invoice);
            model.SaleResponse = VPOSClient.Sale(saleRequest, _nestpayHalkBank);
            return View(model);
        }

        public async Task<IActionResult> CallBack(IFormCollection fc)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            Dictionary<string, object> pairs = Request.Form.Keys.ToDictionary(k => k, v => (object)Request.Form[v]);
            var asdasd = JsonConvert.SerializeObject(pairs);
            var asda = fc["mdStatus"];
            var errorMessage = fc["mdErrorMsg"];
            var CARDISSUER = fc["EXTRA.CARDISSUER"];

            var txstatus = fc["txstatus"];
            var Response = txstatus.ToString() == "N" || txstatus.ToString() == "R" ? "3D Ekranında Red Edildi" : fc["Response"].ToString();
            var invoiceNumber = fc["ReturnOid"];
            if (invoiceNumber.ToString().Length == 0)
                invoiceNumber = fc["oid"].ToString();
            var maskedCreditCard = fc["maskedCreditCard"];

            var errorHash = fc["md"];
            var payment = _paymentRepository.Get(x => x.ReturnId == invoiceNumber.ToString()).Result.Data;
            if (payment == null)
                payment = new Payments();

            payment.Amount = Convert.ToDecimal(fc["amount"].ToString().Replace(".", ","));
            payment.ClientIp = fc["clientIp"].ToString();
            payment.ReturnId = invoiceNumber.ToString();
            payment.CardIssuer = fc["EXTRA.CARDISSUER"].ToString();
            payment.ClientIp = fc["clientIp"].ToString();
            payment.Response = Response.ToString();
            payment.CardBrand = fc["EXTRA.CARDBRAND"].ToString();
            payment.MaskedCreditCard = fc["maskedCreditCard"].ToString();
            payment.MerchantName = fc["merchantName"].ToString();
            payment.Xid = fc["xid"].ToString();
            payment.Installment = Convert.ToInt32(fc["installment"].ToString() == "" ? 1 : fc["installment"].ToString());
            if (invoiceNumber.ToString().Length == 0)
            {
                return Redirect("/manager/500/Ödeme alınırken bir hata meydana geldi");
            }
            var invoice = _invoiceRepository.Get(x => x.ReturnId == invoiceNumber.ToString()).Result.Data;


            if (invoice != null)
            {
                payment.InvoiceId = invoice.ItemGuid;

                invoice.InvoiceStatuId = txstatus.ToString() == "Y" ? Convert.ToInt32(InvoiceStatu.Odendi) : Convert.ToInt32(InvoiceStatu.HataliIslem);
                await _invoiceRepository.UpdateAsync(invoice);
                await _paymentRepository.AddAsync(payment);

                return Redirect("/client/invoices/detail?id=" + invoice.ItemGuid);


            }
            else
            {
                await _paymentRepository.UpdateAsync(payment);
                return Redirect("/client/payments/history");

            }



            return View();
        }
        public IActionResult success(IFormCollection fc)
        {
            Dictionary<string, object> pairs = Request.Form.Keys.ToDictionary(k => k, v => (object)Request.Form[v]);


            return View();
        }
        public IActionResult fail(IFormCollection fc)
        {
            Dictionary<string, object> pairs = Request.Form.Keys.ToDictionary(k => k, v => (object)Request.Form[v]);


            return View();
        }


        public IActionResult cancel()
        {
            return View();
        }
    }
}
