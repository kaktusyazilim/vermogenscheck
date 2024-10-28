using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Sms
{
    public class SmsSetting
    {
        public string JsonValue { get; set; } = string.Empty;
        public int SmsTypeId { get; set; }
    }
}
