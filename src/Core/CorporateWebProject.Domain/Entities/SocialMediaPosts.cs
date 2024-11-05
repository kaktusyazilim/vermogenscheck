using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class SocialMediaPosts:EntityBase
    {
        public int Queue { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Photo { get; set; } = string.Empty;
        public string FriendlyUrl { get; set; } = string.Empty;
        public string SocialMedia { get; set; } = string.Empty;
    }
}
