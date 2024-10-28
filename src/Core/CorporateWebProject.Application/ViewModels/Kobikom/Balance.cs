using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.SmsService.Entitiy.Kobikom
{
    public class Balance
    {
        public decimal balance { get; set; }
        public string User { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public int cost { get; set; }
        public string code { get; set; } = string.Empty;
    }
}
