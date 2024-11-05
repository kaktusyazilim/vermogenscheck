using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Jobs : EntityBase
    {
        public string CompanyGuid { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string SortDescription { get; set; } = string.Empty;
        public string FullDescription { get; set; } = string.Empty;
        public string TimeType { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsUrgent { get; set; }
    }
}
