using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.Payment.Iyzico.Entities
{
    public class BasketItem
    {
        public string Id { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category1 { get; set; } = string.Empty;
        public string Category2 { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public string SubMerchantKey { get; set; } = string.Empty;
        public string SubMerchantPrice { get; set; } = string.Empty;
    }
}
