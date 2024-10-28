using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Projects:EntityBase
    {
        public int LangId { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }
        public int LocationId { get; set; }
        public int Queue { get; set; }
        public string Title { get; set; }=string.Empty;
        public string FriendlyUrl { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string MetaDescription { get; set; }
        public string Icon { get; set; }
    }
}
