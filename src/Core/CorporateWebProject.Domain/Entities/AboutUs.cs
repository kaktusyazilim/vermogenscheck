using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class AboutUs:EntityBase
    {
        [Required(ErrorMessage ="Lütfen bir açıklama giriniz")]
        public string Description { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        [Required(ErrorMessage = "Lütfen butona bir başlık giriniz.")]

        public string Button1Text { get; set; } = string.Empty;
        [Required(ErrorMessage = "Lütfen butona bir renk giriniz")]

        public string Button1Color { get; set; } = string.Empty;
        [Required(ErrorMessage = "Lütfen butona bir yönlendirme linki giriniz")]

        public string Button1Url { get; set; } = string.Empty;
    }
}
