using CorporateWebProject.Application.ViewModels.Iyzico;
using CorporateWebProject.Infrastructure.Payment.Iyzico.Entities;
using CorporateWebProject.Infrastructure.Payment.Iyzico.ViewModels;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.Payment.Abstract
{
    public interface IIyzicoService
    {
        public Options Connect(Connection model);
        public bool PaymentInitialize(PaymentRequestVM PaymentRequestVM, out ThreedsInitialize Value);
        public bool PaymentWithRegisterCard(PaymentRequestVM PaymentRequestVM, out Iyzipay.Model.Payment Value);
        public bool PaymentWithCheckoutForm(PaymentRequestCheckoutFormVM PaymentRequestVM, out CheckoutFormInitialize Value);
        public CardList GetCardInformation(Connection Connection, string CardUserKey);
        public CheckoutForm GetFormResult(Connection Connection, RetrieveCheckoutFormRequest Request);
        public ThreedsPayment GetResult(Connection Connection, RetrievePaymentRequest Request);
        public InstallmentInfo GetInstallment(Connection Connection, RetrieveInstallmentInfoRequest Request);
        public Refund RefundRequest(Connection Connection, CreateRefundRequest request);
        public Cancel Cancelrequest(Connection Connection, CreateCancelRequest request);
    }
}
