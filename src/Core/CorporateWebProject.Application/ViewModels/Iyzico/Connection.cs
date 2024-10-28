using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.Payment.Iyzico.Entities
{
    public class Connection
    {
        #region IyzicoConnection
        public int Id { get; set; }
        public string ApiKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string SuccessUrl { get; set; } = string.Empty;
        public string FailUrl { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        #endregion
    }
}
