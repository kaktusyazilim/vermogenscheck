using CorporateWebProject.Infrastructure.Payment.Iyzico.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.ViewModels.Iyzico
{
    public class PaymentRequestCheckoutFormVM
    {
        private string _currency = "TRY";

        public string OrderId { get; set; } = string.Empty;
        public string BasketId { get; set; } = string.Empty;
        public string CallbackUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get { return _currency; } set { value = value == null ? value = _currency : _currency = value; } }
        public decimal PaymentAmount { get; set; }
        public Connection Connection { get; set; } = new();
        public Buyer Buyer { get; set; } = new();
        public Address ShippingAddress { get; set; } = new();
        public Address BillingAddress { get; set; } = new();
        public List<BasketItem> BasketItems { get; set; } = new();
    }
}
