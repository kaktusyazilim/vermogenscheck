using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Accounts
{
    public class VertificationRequestDTO
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Phone { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public int Type { get; set; } 
    }
}
