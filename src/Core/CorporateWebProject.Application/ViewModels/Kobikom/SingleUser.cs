using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.SmsService.Entitiy.Kobikom
{
    public class SingleUser
    {
        private string _from = "";
        private int _unicode = 1;
        public string from { get { return _from; } set { if (value == "") _from = ""; else _from = value; } }
        public string to { get; set; }
        public string sms { get; set; }
        public int unicode { get { return _unicode; } set { if (value == 0) _unicode = 0; else _unicode = value; } }
        //protected SingleUser()
        //{
        //    KobikomManagement kob = new KobikomManagement();
        //    _from = kob.GetTitles()[0].sender_id;
        //}
    }
}
