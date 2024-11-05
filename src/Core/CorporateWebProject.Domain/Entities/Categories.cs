using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Categories:EntityBase
    {
        public int LangId { get; set; }
        public int Queue { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string Name { get; set; }=string.Empty;
        public string FriendlyUrl { get; set; } = string.Empty;
        public string FullDescription { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Gallery { get; set; } = string.Empty;
        public bool IsShow { get; set; }
        public bool IsLink { get; set; }
        public bool IsPage { get; set; }
        public bool IsModul { get; set; }


    }
}
