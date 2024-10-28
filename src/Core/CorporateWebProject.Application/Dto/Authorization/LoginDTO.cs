using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Authorization
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string Password { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public string FirebaseKey { get; set; } = string.Empty;
    }
}
