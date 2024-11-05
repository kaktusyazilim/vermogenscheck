using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Documents:EntityBase
    {
        public string FolderGuid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Size { get; set; } 
        public string FilePath { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
    }
}
