using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Accounts
{
    public class ProfileDTO
    {
        public string AccountGuid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Description { get; set; }=string.Empty;
        public string Mail { get; set; }=string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.Now;
    }
}
