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
        public int LangId { get; set; }
        [Required(ErrorMessage ="Lütfen bir başlık giriniz")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage ="Lütfen bir açıklama giriniz")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage ="Lütfen bir resim seçiniz")]
        public string Image { get; set; } = string.Empty;
    }
}
