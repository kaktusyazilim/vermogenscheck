using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class OfficialDocuments:EntityBase
    {
        public string CategoryGuid { get; set; } = string.Empty;
        public string SubCategoryGuid { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Name { get; set; }=string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public bool IsSubTitle { get; set; }

    }
}
