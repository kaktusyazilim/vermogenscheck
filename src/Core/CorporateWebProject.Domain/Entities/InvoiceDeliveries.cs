using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class InvoiceDeliveries:EntityBase
    {
        public string StudentGuid { get; set; } = string.Empty;
        public string CreateUserGuid { get; set; } = string.Empty;
        public string CreateUserName { get; set; } = string.Empty;
        public string ModifiedUserGuid { get; set; } = string.Empty;
        public string ModifiedUserName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Logs { get; set; } = string.Empty;
        public string InvoiceDetailsJSON { get; set; } = string.Empty;
    }
}
