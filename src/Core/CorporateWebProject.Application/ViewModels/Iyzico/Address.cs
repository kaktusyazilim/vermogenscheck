using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.Payment.Iyzico.Entities
{
    public class Address
    {
        [JsonProperty(PropertyName = "Address")]
        public string Content { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
