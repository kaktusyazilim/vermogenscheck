using CorporateWebProject.Application.Dto.Sms;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.Infrastructure.SmsService.Common;
using CorporateWebProject.Infrastructure.SmsService.Entitiy.Kobikom;
using CorporateWebProject.Infrastructure.SmsService.Entitiy.NetGSM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.SmsService.Handler
{
    public class BaseSmsService:IBaseSmsService
    {
        private readonly IKobikomService _kobikomHandler;
        private readonly INetgsmService _netGsmHandler;

        public BaseSmsService(IKobikomService kobikomHandler, INetgsmService netGsmHandler)
        {
            _kobikomHandler = kobikomHandler;
            _netGsmHandler = netGsmHandler;
        }

        public bool SendMessage(CorporateWebProject.Application.Dto.Sms.SmsSetting smsSetting, string message, string phone)
        {
            if (smsSetting != null)
            {
                switch (smsSetting.SmsTypeId)
                {
                    case 1:
                        var netGsmModel = JsonConvert.DeserializeObject<NetGsmJsonDTO>(smsSetting.JsonValue);
                        return _netGsmHandler.SendMessages(new NetGsmMessage()
                        {
                            bayikodu = netGsmModel!.Username,
                            encoding = "TR",
                            password = netGsmModel.Password,
                            header = netGsmModel.Title,
                            msg = message,
                            msgs = null!,
                            username = netGsmModel.Username,
                            gsm = new string[] { phone },
                        });
                    case 0:
                        return _kobikomHandler.SendMessage(JsonConvert.DeserializeObject<KobikomJsonDTO>(smsSetting.JsonValue)!, message, phone).code=="ok";
                    default:
                        return false;
                }
            }
            return false;
        }
    }
}
