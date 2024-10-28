using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class InvoiceDetails : EntityBase
    {
        public string InvoiceGuid { get; set; } = string.Empty;
        public string ProductGuid { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public string CompanyCode { get; set; }=string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductShortDetails { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string UnitType { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime InvoiceDate { get; set; }

    }
}
