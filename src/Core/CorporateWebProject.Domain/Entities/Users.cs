using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Users:EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CompanyPhone { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }
        public DateTime JobStartDate { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsAuthRegulation { get; set; }
        public bool IsManager { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsListHide { get; set; }
        public bool IsWorkTable { get; set; }

    }

    
}
