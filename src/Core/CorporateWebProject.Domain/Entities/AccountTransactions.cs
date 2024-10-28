using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class AccountTransactions:EntityBase
    {
        public string AccountGuid { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public decimal Amount { get; set; } = 0.0m;
        public decimal AccountAmount { get; set; } = 0.0m;
        public string ReceiptNumber { get; set; } = string.Empty;
        public string Files { get; set; } = string.Empty;
        public string CreateUserGuid { get; set; } = string.Empty;
    }
}
