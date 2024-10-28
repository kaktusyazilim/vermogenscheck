using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Alarms:EntityBase
    {
        public string UserGuid { get; set; } = string.Empty;
        public string ClientGuid { get; set; } = string.Empty;
        public DateTime AlarmDate { get; set; }
        public DateTime AlarmTime { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
}
