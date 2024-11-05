using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Banks:EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string ShorCode { get; set; } = string.Empty;
        public string BankCode { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
