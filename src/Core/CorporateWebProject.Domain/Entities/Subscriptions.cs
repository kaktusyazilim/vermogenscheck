using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Subscriptions:EntityBase
    {
        public string Mail { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
    }
}
