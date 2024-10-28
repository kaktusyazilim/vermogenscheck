using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class LowestCategory:EntityBase
    {
        public int LangId { get; set; }
        public int TypeId { get; set; }
        public int SubcategoryId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryGuid { get; set; } = string.Empty;
        public string SubcategoryGuid { get; set; } = string.Empty;
        public int Queue { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FriendlyUrl { get; set; } = string.Empty;
        public string FullDescription { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string SubCategoryName { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Gallery { get; set; } = string.Empty;

        public bool IsShow { get; set; }
        public bool IsLink { get; set; }
        public bool IsPage { get; set; }
        public bool IsModul { get; set; }

    }
}
