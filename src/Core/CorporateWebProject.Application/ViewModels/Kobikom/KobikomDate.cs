using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.SmsService.Entitiy.Kobikom
{
    public class KobikomDate
    {
        public DateTime date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; } = string.Empty;
    }
}
