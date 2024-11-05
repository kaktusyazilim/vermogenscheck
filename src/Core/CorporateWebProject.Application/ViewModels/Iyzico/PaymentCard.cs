using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.Payment.Iyzico.Entities
{
    public class PaymentCard
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Bu alan 3 ile 100 karakter arasında olmalı.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string CardHolderName { get; set; }=string.Empty;
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        [DataType(DataType.Text)]
        public string CardNumber { get; set; }  =string.Empty;
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        [DataType(DataType.Text)]
        public string ExpireYear { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        [DataType(DataType.Text)]
        public string ExpireMonth { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        [DataType(DataType.Text)]
        public string Cvc { get; set; } = string.Empty;
        public int? RegisterCard { get; set; }
        public string CardAlias { get; set; } = string.Empty;
        public string CardToken { get; set; } = string.Empty;
        public string CardUserKey { get; set; } = string.Empty;
    }
}
