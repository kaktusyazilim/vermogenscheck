using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.New
{
    public class NewItemDTO
    {
        public string Id { get; set; }=string.Empty;
        public string Title { get; set; }=string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime PublishingDate { get; set; }
        public string PublishingDateString { get; set; } = string.Empty;
        public string Link { get; set; }=string.Empty;
    }
}
