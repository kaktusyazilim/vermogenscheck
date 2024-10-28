using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Districts:EntityBase
    {
        public string District { get; set; } = string.Empty;
        public int City { get; set; }
    }
}
