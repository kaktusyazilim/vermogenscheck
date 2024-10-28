using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Cities:EntityBase
    {
        public string City { get; set; } = string.Empty;
    }
}
