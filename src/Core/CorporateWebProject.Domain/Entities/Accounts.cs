using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Accounts:EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string CurrencyType { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string BankBranch { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public string IBAN { get; set; } = string.Empty;
        public string CreateUserGuid { get; set; } = string.Empty;
    }
}
