using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbeveynApp.Application.Dto.VirtualPos
{
    public class PaytrJsonDTO
    {
        public string merchantId { get; set; } = string.Empty;
        public string merchantKey { get; set; } = string.Empty;
        public string merchantSalt { get; set; } = string.Empty;
    }
}
