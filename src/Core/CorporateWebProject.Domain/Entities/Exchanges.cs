using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Exchanges:EntityBase
    {
        public int LangId { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage ="Lütfen yüzde kısmını giriniz")]
        public decimal Count { get; set; }
        public string Description { get; set; } = string.Empty;
        public string OldCounts { get; set; } = string.Empty;
    }
}
