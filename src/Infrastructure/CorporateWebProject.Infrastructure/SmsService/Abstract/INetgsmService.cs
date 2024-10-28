using CorporateWebProject.Infrastructure.SmsService.Entitiy.NetGSM;

namespace CorporateWebProject.Infrastructure.SmsService.Common
{
    public interface INetgsmService
    {
        public bool SendMessages(NetGsmMessage model);
    }
}
