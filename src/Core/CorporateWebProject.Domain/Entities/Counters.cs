using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Counters:EntityBase
    {
        public int LangId { get; set; }
        public int Queue { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
        public string Icon { get; set; } = string.Empty;
    }
}
