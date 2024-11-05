using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Common
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
          
        }
        public int Id { get; set; }
        public string ItemGuid { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsPassive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
