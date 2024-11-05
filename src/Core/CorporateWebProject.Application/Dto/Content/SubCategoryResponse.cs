using CorporateWebProject.Application.Dto.Content.Items.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Content
{
    public class SubCategoryResponse
    {

        public string ItemGuid { get; set; } = string.Empty;
        public string CategoryGuid { get; set; } = string.Empty;
        public int Queue { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Type { get; set; }
        public string Photo { get; set; } = string.Empty;
        public IBaseItem? Item { get; set; }
    }
}
