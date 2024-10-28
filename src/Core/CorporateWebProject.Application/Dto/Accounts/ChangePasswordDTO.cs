using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Accounts
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage ="Bu alan boş geçilemez.")]
        public string AccountGuid { get; set; }=string.Empty;
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string NewPassword { get; set; }= string.Empty;
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        [Compare("NewPassword",ErrorMessage ="Şifreleriniz birbiriyle uyuşmamaktadır.")]
        public string RepeatPassword { get; set; } = string.Empty;
    }
}
