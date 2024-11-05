using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Content
{
    public class CategoryResponse
    {
        public string ItemGuid { get; set; } = string.Empty;
        public int Queue { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<SubCategoryResponse> SubCategories { get; set; } = new();
    }
}
