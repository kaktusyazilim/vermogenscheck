using CorporateWebProject.Application.Dto.Sms;
using CorporateWebProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.SmsService.Common
{
    public interface IBaseSmsService
    {
        public bool SendMessage(CorporateWebProject.Application.Dto.Sms.SmsSetting smsSetting, string message, string phone);
    }
}
