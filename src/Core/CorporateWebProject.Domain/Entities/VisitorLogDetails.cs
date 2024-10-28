using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class VisitorLogDetails:EntityBase
    {
        public string UserToken { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
