using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class FolderPermissions:EntityBase
    {
        public string CompanyGuid { get; set; } = string.Empty;
        public string FolderGuid { get; set; } = string.Empty;
        public bool AuthRead { get; set; }
        public bool AuthCreate { get; set; }
        public bool AuthPassive { get; set; }
        public bool AuthDelete { get; set; }
    }
}
