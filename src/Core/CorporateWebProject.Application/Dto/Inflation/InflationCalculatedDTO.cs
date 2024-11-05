using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Inflation
{
    public class InflationCalculatedDTO
    {
        public string Date { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string DisplayPrice { get; set; } = string.Empty;
        public bool IsToday { get; set; }
    }
}
