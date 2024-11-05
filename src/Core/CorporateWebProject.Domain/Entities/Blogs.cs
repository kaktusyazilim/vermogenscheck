using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Blogs:EntityBase
    {
        public int LangId { get; set; }
        public int CategoryId { get; set; }
        public string FriendlyUrl { get; set; } = string.Empty;
        public string Title { get; set; }  =string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string FullDescription { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public string Gallery { get; set; } = string.Empty;

        public int CreatedUserId { get; set; }
        public string Tags { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public bool IsHomePage { get; set; }
        public bool IsPin { get; set; }
    }
}
