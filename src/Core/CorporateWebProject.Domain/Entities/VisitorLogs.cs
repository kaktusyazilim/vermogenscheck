using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class VisitorLogs:EntityBase
    {
        public string UserToken { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string Browser { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
    }
}
