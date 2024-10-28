using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Modules:EntityBase
    {
        public int LangId { get; set; }
        public int Queue { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsShow { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }
    }
}
