using CorporateWebProject.Application.ViewModels.Iyzico;
using CorporateWebProject.Infrastructure.Payment.Abstract;
using CorporateWebProject.Infrastructure.Payment.Enums;
using CorporateWebProject.Infrastructure.Payment.Iyzico.Entities;
using CorporateWebProject.Infrastructure.Payment.Iyzico.ViewModels;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.Payment.Concrete
{
    public class IyzicoService : IIyzicoService
    {
        public Options Option { get; set; } = new();
        public string CallbackUrl { get; set; } = string.Empty;

        public Options Connect(Connection model)
        {
            try
            {
                Options option = new Options();
                option.ApiKey = model.ApiKey;
                option.BaseUrl = model.BaseUrl;
                option.SecretKey = model.SecretKey.Replace("\r\n", "");
                return option;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public bool PaymentInitialize(PaymentRequestVM PaymentRequestVM, out ThreedsInitialize Value)
        {
            CreatePaymentRequest request = new CreatePaymentRequest()
            {
                ConversationId = PaymentRequestVM.OrderId,
                Installment = PaymentRequestVM.Installment,
                BasketId = PaymentRequestVM.BasketId,
                CallbackUrl = PaymentRequestVM.CallbackUrl,
                Locale = "TR",
                Currency = PaymentRequestVM.Currency,
                Price = string.Format("{0:0.00}", PaymentRequestVM.BasketItems.Sum(x => x.Price * x.Quantity).ToString().Replace(",", ".")),
                PaidPrice = string.Format("{0:0.00}", PaymentRequestVM.BasketItems.Sum(x => x.Price * x.Quantity).ToString().Replace(",", ".")),
                PaymentCard = new Iyzipay.Model.PaymentCard()
                {
                    CardHolderName = PaymentRequestVM.PaymentCard.CardHolderName,
                    CardNumber = PaymentRequestVM.PaymentCard.CardNumber.Replace(" ", "").Replace("-", ""),
                    Cvc = PaymentRequestVM.PaymentCard.Cvc,
                    ExpireMonth = PaymentRequestVM.PaymentCard.ExpireMonth,
                    ExpireYear = PaymentRequestVM.PaymentCard.ExpireYear,
                    RegisterCard = Convert.ToInt16(PaymentRequestVM.PaymentCard.RegisterCard),
                },
                BillingAddress = new Iyzipay.Model.Address()
                {
                    City = PaymentRequestVM.BillingAddress.City,
                    ContactName = PaymentRequestVM.BillingAddress.ContactName,
                    Country = PaymentRequestVM.BillingAddress.Country,
                    Description = PaymentRequestVM.BillingAddress.Content,
                    ZipCode = PaymentRequestVM.BillingAddress.ZipCode
                },
                Buyer = new Iyzipay.Model.Buyer()
                {
                    City = PaymentRequestVM.Buyer.City,
                    ZipCode = PaymentRequestVM.Buyer.ZipCode.ToString(),
                    Country = PaymentRequestVM.Buyer.Country,
                    Email = PaymentRequestVM.Buyer.Email,
                    GsmNumber = PaymentRequestVM.Buyer.GsmNumber,
                    Id = PaymentRequestVM.Buyer.Id,
                    IdentityNumber = PaymentRequestVM.Buyer.IdentityNumber,
                    Ip = PaymentRequestVM.Buyer.Ip,
                    LastLoginDate = PaymentRequestVM.Buyer.LastLoginDate,
                    Name = PaymentRequestVM.Buyer.Name,
                    RegistrationAddress = PaymentRequestVM.Buyer.RegistrationAddress,
                    RegistrationDate = PaymentRequestVM.Buyer.RegistrationDate,
                    Surname = PaymentRequestVM.Buyer.Surname,
                },
            };

            if (PaymentRequestVM.ShippingAddress != null)
                request.ShippingAddress = new Iyzipay.Model.Address()
                {
                    City = PaymentRequestVM.ShippingAddress.City,
                    ContactName = PaymentRequestVM.ShippingAddress.ContactName,
                    Country = PaymentRequestVM.ShippingAddress.Country,
                    Description = PaymentRequestVM.ShippingAddress.Content,
                    ZipCode = PaymentRequestVM.ShippingAddress.ZipCode
                };
            else
                request.ShippingAddress = request.BillingAddress;

            request.BasketItems = new List<Iyzipay.Model.BasketItem>();
            for (int i = 0; i < PaymentRequestVM.BasketItems.Count; i++)
            {
                Iyzipay.Model.BasketItem item = new Iyzipay.Model.BasketItem()
                {
                    Category1 = PaymentRequestVM.BasketItems[i].Category1,
                    Category2 = PaymentRequestVM.BasketItems[i].Category2,
                    Id = PaymentRequestVM.BasketItems[i].Id,
                    ItemType = PaymentRequestVM.BasketItems[i].ItemType.ToString(),
                    Name = PaymentRequestVM.BasketItems[i].Name,
                    Price = string.Format("{0:0.00}", (PaymentRequestVM.BasketItems[i].Price * PaymentRequestVM.BasketItems[i].Quantity)).Replace(",", "."),
                };
                request.BasketItems.Add(item);
            }
            Value = ThreedsInitialize.Create(request, this.Connect(PaymentRequestVM.Connection));

            return (Value.Status == "success");
        }

        public bool PaymentWithCheckoutForm(PaymentRequestCheckoutFormVM PaymentRequestVM, out CheckoutFormInitialize Value)
        {
            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest()
            {
                ConversationId = PaymentRequestVM.OrderId,
                BasketId = PaymentRequestVM.BasketId,
                CallbackUrl = PaymentRequestVM.CallbackUrl,
                Locale = "TR",
                Currency = PaymentRequestVM.Currency,
                Price = string.Format("{0:0.00}", PaymentRequestVM.BasketItems.Sum(x => x.Price * x.Quantity).ToString().Replace(",", ".")),
                PaidPrice = string.Format("{0:0.00}", PaymentRequestVM.BasketItems.Sum(x => x.Price * x.Quantity).ToString().Replace(",", ".")),
                BillingAddress = new Iyzipay.Model.Address()
                {
                    City = PaymentRequestVM.BillingAddress.City,
                    ContactName = PaymentRequestVM.BillingAddress.ContactName,
                    Country = PaymentRequestVM.BillingAddress.Country,
                    Description = PaymentRequestVM.BillingAddress.Content,
                    ZipCode = PaymentRequestVM.BillingAddress.ZipCode
                },
                Buyer = new Iyzipay.Model.Buyer()
                {
                    City = PaymentRequestVM.Buyer.City,
                    ZipCode = PaymentRequestVM.Buyer.ZipCode.ToString(),
                    Country = PaymentRequestVM.Buyer.Country,
                    Email = PaymentRequestVM.Buyer.Email,
                    GsmNumber = PaymentRequestVM.Buyer.GsmNumber,
                    Id = PaymentRequestVM.Buyer.Id,
                    IdentityNumber = PaymentRequestVM.Buyer.IdentityNumber,
                    Ip = PaymentRequestVM.Buyer.Ip,
                    LastLoginDate = PaymentRequestVM.Buyer.LastLoginDate,
                    Name = PaymentRequestVM.Buyer.Name,
                    RegistrationAddress = PaymentRequestVM.Buyer.RegistrationAddress,
                    RegistrationDate = PaymentRequestVM.Buyer.RegistrationDate,
                    Surname = PaymentRequestVM.Buyer.Surname,
                },
            };

            if (PaymentRequestVM.ShippingAddress != null)
                request.ShippingAddress = new Iyzipay.Model.Address()
                {
                    City = PaymentRequestVM.ShippingAddress.City,
                    ContactName = PaymentRequestVM.ShippingAddress.ContactName,
                    Country = PaymentRequestVM.ShippingAddress.Country,
                    Description = PaymentRequestVM.ShippingAddress.Content,
                    ZipCode = PaymentRequestVM.ShippingAddress.ZipCode
                };
            else
                request.ShippingAddress = request.BillingAddress;

            request.BasketItems = new List<Iyzipay.Model.BasketItem>();
            for (int i = 0; i < PaymentRequestVM.BasketItems.Count; i++)
            {
                Iyzipay.Model.BasketItem item = new Iyzipay.Model.BasketItem()
                {
                    Category1 = PaymentRequestVM.BasketItems[i].Category1,
                    Category2 = PaymentRequestVM.BasketItems[i].Category2,
                    Id = PaymentRequestVM.BasketItems[i].Id,
                    ItemType = PaymentRequestVM.BasketItems[i].ItemType.ToString(),
                    Name = PaymentRequestVM.BasketItems[i].Name,
                    Price = string.Format("{0:0.00}", (PaymentRequestVM.BasketItems[i].Price * PaymentRequestVM.BasketItems[i].Quantity)).Replace(",", "."),
                };
                request.BasketItems.Add(item);
            }
            Value = CheckoutFormInitialize.Create(request, this.Connect(PaymentRequestVM.Connection));

            return (Value.Status == "success");
        }

        public bool PaymentWithRegisterCard(PaymentRequestVM PaymentRequestVM, out Iyzipay.Model.Payment Value)
        {
            CreatePaymentRequest request = new CreatePaymentRequest()
            {
                ConversationId = PaymentRequestVM.OrderId,
                Installment = PaymentRequestVM.Installment,
                BasketId = PaymentRequestVM.BasketId,
                Locale = "TR",
                Currency = PaymentRequestVM.Currency,
                Price = PaymentRequestVM.BasketItems.Sum(x => x.Price * x.Quantity).ToString().Replace(",", "."),
                PaidPrice = PaymentRequestVM.BasketItems.Sum(x => x.Price * x.Quantity).ToString().Replace(",", "."),
                PaymentCard = new Iyzipay.Model.PaymentCard()
                {
                    CardUserKey = PaymentRequestVM.PaymentCard.CardUserKey,
                    CardToken = PaymentRequestVM.PaymentCard.CardToken
                },
                BillingAddress = new Iyzipay.Model.Address()
                {
                    City = PaymentRequestVM.BillingAddress.City,
                    ContactName = PaymentRequestVM.BillingAddress.ContactName,
                    Country = PaymentRequestVM.BillingAddress.Country,
                    Description = PaymentRequestVM.BillingAddress.Content,
                    ZipCode = PaymentRequestVM.BillingAddress.ZipCode
                },
                Buyer = new Iyzipay.Model.Buyer()
                {
                    City = PaymentRequestVM.Buyer.City,
                    ZipCode = PaymentRequestVM.Buyer.ZipCode.ToString(),
                    Country = PaymentRequestVM.Buyer.Country,
                    Email = PaymentRequestVM.Buyer.Email,
                    GsmNumber = PaymentRequestVM.Buyer.GsmNumber,
                    Id = PaymentRequestVM.Buyer.Id,
                    IdentityNumber = PaymentRequestVM.Buyer.IdentityNumber,
                    Ip = PaymentRequestVM.Buyer.Ip,
                    LastLoginDate = PaymentRequestVM.Buyer.LastLoginDate,
                    Name = PaymentRequestVM.Buyer.Name,
                    RegistrationAddress = PaymentRequestVM.Buyer.RegistrationAddress,
                    RegistrationDate = PaymentRequestVM.Buyer.RegistrationDate,
                    Surname = PaymentRequestVM.Buyer.Surname,
                },
            };
            if (PaymentRequestVM.BasketItems.Find(x => x.ItemType == ItemType.PHYSICAL.ToString()) != null)
            {
                request.ShippingAddress = new Iyzipay.Model.Address()
                {
                    City = PaymentRequestVM.ShippingAddress.City,
                    ContactName = PaymentRequestVM.ShippingAddress.ContactName,
                    Country = PaymentRequestVM.ShippingAddress.Country,
                    Description = PaymentRequestVM.ShippingAddress.Content,
                    ZipCode = PaymentRequestVM.ShippingAddress.ZipCode
                };
            }
            else
                request.ShippingAddress = request.BillingAddress; // EĞER IYZICO TARAFINDA SHIPPINGADDRESS ZORUNLU İSE FATURA ADRESİNİ GÖNDERMEYİ TERCİH ETTİM.ÜRÜN TİPİ SANAL OLDUĞU İÇİN FİZİKSEL BİR ÜRÜN YOK.
            request.BasketItems = new List<Iyzipay.Model.BasketItem>();
            for (int i = 0; i < PaymentRequestVM.BasketItems.Count; i++)
            {
                Iyzipay.Model.BasketItem item = new Iyzipay.Model.BasketItem()
                {
                    Category1 = PaymentRequestVM.BasketItems[i].Category1,
                    Category2 = PaymentRequestVM.BasketItems[i].Category2,
                    Id = PaymentRequestVM.BasketItems[i].Id,
                    ItemType = PaymentRequestVM.BasketItems[i].ItemType.ToString(),
                    Name = PaymentRequestVM.BasketItems[i].Name,
                    Price = string.Format("{0:0.00}", (PaymentRequestVM.BasketItems[i].Price * PaymentRequestVM.BasketItems[i].Quantity)).Replace(",", "."),

                };
                request.BasketItems.Add(item);
            }
            Value = Iyzipay.Model.Payment.Create(request, this.Connect(PaymentRequestVM.Connection));

            return (Value.Status == "success");
        }

        public CardList GetCardInformation(Connection Connection, string CardUserKey) => CardList.Retrieve(new() { Locale = Locale.TR.ToString(), CardUserKey = CardUserKey, }, this.Connect(Connection));
        public ThreedsPayment GetResult(Connection Connection, RetrievePaymentRequest Request) => ThreedsPayment.Create(new() { PaymentId = Request.PaymentId, Locale = Locale.TR.ToString(), ConversationId = Request.ConversationId }, Connect(Connection));
        public CheckoutForm GetFormResult(Connection Connection, RetrieveCheckoutFormRequest Request) => CheckoutForm.Retrieve(Request, Connect(Connection));
        public InstallmentInfo GetInstallment(Connection Connection, RetrieveInstallmentInfoRequest Request) => InstallmentInfo.Retrieve(Request, Connect(Connection));
        public Refund RefundRequest(Connection Connection, CreateRefundRequest request) => Refund.Create(request, Connect(Connection));
        public Cancel Cancelrequest(Connection Connection, CreateCancelRequest request) => Cancel.Create(request, Connect(Connection));

    }
}
