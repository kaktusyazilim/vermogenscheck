using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class BlogCategories:EntityBase
    {
        public string BlogGuid { get; set; }=string.Empty;
        public string CategoryGuid { get; set; }=string.Empty;
    }
}
