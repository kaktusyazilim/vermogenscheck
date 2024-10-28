using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Notification
{
    public class NotificationDTO
    {
        public string ItemGuid { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }=string.Empty;
    }
}
