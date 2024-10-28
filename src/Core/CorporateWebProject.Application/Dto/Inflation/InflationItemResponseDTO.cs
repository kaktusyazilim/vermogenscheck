using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Inflation
{
    public class InflationItemResponseDTO
    {
        public string ItemGuid { get; set; } = string.Empty;
        public string CategoryGuid { get; set; } = string.Empty;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public decimal Inflation { get; set; }
        public DateTime InflationDate { get; set; } = DateTime.Now;
    }
}
