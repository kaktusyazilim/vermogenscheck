using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Folders:EntityBase
    {
        public string CompanyGuid { get; set; } = string.Empty;
        public string TopGuid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public bool IsGeneral { get; set; }
    }
}
