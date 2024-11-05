using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Companies:EntityBase
    {
        public int CategoryId { get; set; }
        public int LangId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string TaxAdministrator { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string TopPhone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string AuthorizedName { get; set; } = string.Empty;
        public string AuthorizedSurname { get; set; } = string.Empty;
        public string AuthorizedMail { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public string GroupCode { get; set; } = string.Empty;
        public bool IsTopCompany { get; set; }
        public bool IsWebSiteShow { get; set; }
    }
}
