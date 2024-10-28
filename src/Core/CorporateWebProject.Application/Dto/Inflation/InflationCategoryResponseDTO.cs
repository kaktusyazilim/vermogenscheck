using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Inflation
{
    public class InflationCategoryResponseDTO
    {
        public string ItemGuid { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal InflationRate { get; set; }
        public string Inflation { get; set; } = string.Empty;
        public List<InflationItemResponseDTO> Items { get; set; } = new();
        public List<InflationCalculatedDTO> Calculateds { get; set; } = new();
    }
}
