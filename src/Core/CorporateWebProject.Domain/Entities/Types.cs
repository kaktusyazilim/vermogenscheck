using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Types:EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;

        public string Icon { get; set; }= string.Empty;
        public bool IsPage { get; set; }
        public int ModulId { get; set; }
    }
}
