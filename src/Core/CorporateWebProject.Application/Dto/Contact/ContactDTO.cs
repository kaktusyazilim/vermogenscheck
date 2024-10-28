using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Contract
{
    public class ContactDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Lütfen mail adresinizi giriniz")]
        public string Mail { get; set; } = string.Empty;
        [Required(ErrorMessage = "Lütfen telefon numaranızı giriniz.")]
        public string Phone { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
