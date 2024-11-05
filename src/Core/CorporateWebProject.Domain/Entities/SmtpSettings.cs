using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class SmtpSettings:EntityBase
    {
        public string Server { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string MailPassword { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
