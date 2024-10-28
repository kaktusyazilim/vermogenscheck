using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class PageValues:EntityBase
    {
        public int LangId { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public int ReleatedId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public int ValueType { get; set; }
        public string FriendlyUrl { get; set; } = string.Empty;
        public bool IsShow { get; set; }
    }
}
