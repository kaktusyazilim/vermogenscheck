using CorporateWebProject.Infrastructure.SmsService.Common;
using CorporateWebProject.Infrastructure.SmsService.Entitiy.NetGSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.SmsService.Concrete
{
    public class NetgsmService : INetgsmService
    {
        public bool SendMessages(NetGsmMessage model)
        {
            using (NetGSM_Sms.smsnnClient sms = new NetGSM_Sms.smsnnClient())
                return sms.smsGonder1NV2Async(model.username, model.password, model.header, model.msg, model.gsm, model.encoding, null, null, model.bayikodu, 0).GetAwaiter().GetResult().@return.Length > 4 ? true : false;
        }
    }
}
