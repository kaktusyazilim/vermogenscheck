using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Proposal
{
    public class ProposalDTO
    {
        public int Id { get; set; }
        public string Services { get; set; }
        [Required(ErrorMessage = "Ad kısmı zorunludur")]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required(ErrorMessage = "Mail kısmı zorunludur")]
        [EmailAddress(ErrorMessage = "Lütfen doğru bir mail adresi giriniz")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Mail kısmı zorunludur")]

        [Phone(ErrorMessage = "Lütfen doğru bir telefon numarası giriniz")]
        public string Phone { get; set; }
        public string BrandName { get; set; }
        public string Website { get; set; }

        public string Target { get; set; }
        public string Tehnical { get; set; }
        public string Money { get; set; }
        public string Message { get; set; }
    }
}
