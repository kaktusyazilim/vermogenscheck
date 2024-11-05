using CorporateWebProject.Application.Dto.Content.Items.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Content.Items.Concrete
{
    public class CardItemResponse
    {
        public string SubCategoryGuid { get; set; } = string.Empty;
        public string SubCategoryItemGuid { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }
}
