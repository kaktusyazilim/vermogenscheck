using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class CrmSettings:EntityBase
    {
        public bool EveryoneDataMigration { get; set; }
    }
}
