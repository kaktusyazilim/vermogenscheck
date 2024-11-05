using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Sliders : EntityBase
    {
        [Display(Prompt = "Slider için dil seçiniz")]
        [Required(ErrorMessage = "Lütfen slider için dil alanını boş geçmeyiniz.")]
        public int LangId { get; set; }
        [Display(Prompt = "Slider için bir sıra giriniz")]
        [Required(ErrorMessage = "Lütfen slider sıra alanını boş geçmeyiniz.")]
        public int Queue { get; set; }
        [Display(Prompt = "Slider için bir üst başlık giriniz")]
        [Required(ErrorMessage = "Lütfen üst başlık alanını boş geçmeyiniz.")]
        public string TopTitle { get; set; } = string.Empty;

        [Display(Prompt = "Slider için bir başlık giriniz")]
        [Required(ErrorMessage = "Lütfen başlık alanını boş geçmeyiniz.")]
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        [Display(Prompt = "Slider birinci buton başlığını giriniz.")]
        public string Button1Text { get; set; } = string.Empty;
        [Display(Prompt = "Slider birinci buton rengini giriniz.")]
        public string Button1Color { get; set; } = string.Empty;
        [Display(Prompt = "Slider birinci buton linkini giriniz.")]
        public string Button1Url { get; set; } = string.Empty;
        [Display(Prompt = "Slider ikinici buton başlığını giriniz.")]
        public string Button2Text { get; set; } = string.Empty;
        [Display(Prompt = "Slider ikinici buton rengini giriniz.")]
        public string Button2Color { get; set; } = string.Empty;
        [Display(Prompt = "Slider ikinici buton linkini giriniz.")]
        public string Button2Url { get; set; } = string.Empty;
        public string VideoTitle { get; set; } = string.Empty;

        public string VideoUrl { get; set; } = string.Empty;


        public string FilePath { get; set; } = string.Empty;
    }


}
