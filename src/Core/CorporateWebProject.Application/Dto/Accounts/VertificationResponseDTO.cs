using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Accounts
{
    public class VertificationResponseDTO
    {
        public string AccountGuid { get; set; }=string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
