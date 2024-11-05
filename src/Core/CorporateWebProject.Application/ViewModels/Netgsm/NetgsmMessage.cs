using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.SmsService.Entitiy.NetGSM
{
    public class NetGsmMessage
    {
        public string username { get; set; }=string.Empty;
        public string password { get; set; } = string.Empty;
        public string header { get; set; } = string.Empty;
        public string msg { get; set; } = string.Empty;
        public string[] msgs { get; set; } = new string[0];
        public string[] gsm { get; set; } = new string[0];
        public string encoding { get; set; } = string.Empty;
        public DateTime startdate { get; set; }
        public DateTime stopdate { get; set; }
        public string bayikodu { get; set; } = string.Empty;
        public int filter { get; set; }
    }
}
