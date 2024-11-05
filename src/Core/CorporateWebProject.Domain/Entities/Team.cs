using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Team:EntityBase
    {
        public int Queue { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Facebook { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }
}
