using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Products:EntityBase
    {
        public string StockCode { get; set; }=string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UnityType { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
