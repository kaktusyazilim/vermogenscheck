using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Files:EntityBase
    {
        public string ReleatedId { get; set; }=string.Empty;
        public int TypeId { get; set; }
        public string FilePath { get; set; } = string.Empty;

    }
}
