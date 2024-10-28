using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Invoices:EntityBase
    {
        public int InvoiceStatuId { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string InvoiceNumber { get; set; } = string.Empty;
        public string BankNumber { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal VatPrice { get; set; }
        public int Installment { get; set; }
        public bool Payment3D { get; set; }
        public string Description { get; set; } = string.Empty;
        public string StatuMessage { get; set; } = string.Empty;
        public string ReturnId { get; set; } = string.Empty;
        public string BankId { get; set; } = string.Empty;
        public string maskedCreditCard { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
    }
}
