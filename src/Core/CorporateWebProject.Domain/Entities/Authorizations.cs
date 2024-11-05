using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Authorizations:EntityBase
    {
        public int LangId { get; set; }
        public int UserId { get; set; }
        public int ModulId { get; set; }
        public int PageId { get; set; }
        public string PageGuid { get; set; } = string.Empty;
        public bool AuthRead { get; set; }
        public bool AuthCreate { get; set; }
        public bool AuthUpdate { get; set; }
        public bool AuthDelete { get; set; }
        public int CreateUserId { get; set; }
        public int ModifiedUserId { get; set; }


    }
}
