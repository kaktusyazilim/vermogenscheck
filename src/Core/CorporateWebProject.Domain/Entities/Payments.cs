using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Payments:EntityBase
    {
        public string ReturnId { get; set; } = string.Empty;
        public string CompanyGuid { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public string InvoiceId { get; set; } = string.Empty;
        public string MaskedCreditCard { get; set; } = string.Empty;
        public string CardBrand { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string MerchantName { get; set; } = string.Empty;
        public string ClientIp { get; set; } = string.Empty;
        public string Xid { get; set; } = string.Empty;
        public string CardIssuer { get; set; } = string.Empty;
        public int Installment { get; set; } 
    }
}
