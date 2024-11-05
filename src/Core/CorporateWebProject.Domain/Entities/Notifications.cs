using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Notifications:EntityBase
    {
        public string CompanyGuid { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; }=string.Empty;
        public string Url { get; set; }= string.Empty;
        public DateTime ShowDate { get; set; }
        public bool IsShow { get; set; }
    }
}
