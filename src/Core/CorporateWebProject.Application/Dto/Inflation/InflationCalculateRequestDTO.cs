using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Inflation
{
    public class InflationCalculateRequestDTO
    {
        public string ItemGuid { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public int Type { get; set; }
    }
}
