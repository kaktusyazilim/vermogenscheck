using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class ContactMessages:EntityBase
    {

        public int LangId { get; set; }

        public string CategoryGuid { get; set; } = string.Empty;
        public string Name { get; set; }= string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Message { get; set; }= string.Empty;
        public string IpAddress { get; set; }= string.Empty;
        public bool IsShow { get; set; }
        public int Count { get; set; }
        public string ExchangeGuid { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}
