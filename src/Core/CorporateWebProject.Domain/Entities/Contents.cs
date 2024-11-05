using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Contents :EntityBase
    {
        public int LangId { get; set; } = 1;
        public string ContentKey { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string LongText { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string Video { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }
}
