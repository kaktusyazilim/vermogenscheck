using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Accounts
{
    public class VertificationMatchDTO
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string AccountGuid { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Token { get; set; } = string.Empty;
        public int Type { get; set; }
    }
}
