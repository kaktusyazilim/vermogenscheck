using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.ViewModels.Notification
{
    public class NotificationVM
    {
        public string title { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
        public string icon { get; set; } = string.Empty;
        public string sound { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string main_picture { get; set; }=string.Empty;
    }
}
