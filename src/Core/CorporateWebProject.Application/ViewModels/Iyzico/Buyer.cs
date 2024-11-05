﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.Payment.Iyzico.Entities
{
    public class Buyer
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string GsmNumber { get; set; } = string.Empty;
        public string RegistrationDate { get; set; } = string.Empty;
        public string LastLoginDate { get; set; } = string.Empty;
        public string RegistrationAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;
    }
}
