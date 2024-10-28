using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.ViewModels.Notification
{
    public class FirebaseResultVM
    {
        public long multicast_id { get; set; }
        public bool success { get; set; }
        public bool failure { get; set; }
        public int canonical_ids { get; set; }
        public List<FirebaseMessageVM> results { get; set; } = new();
    }
}
