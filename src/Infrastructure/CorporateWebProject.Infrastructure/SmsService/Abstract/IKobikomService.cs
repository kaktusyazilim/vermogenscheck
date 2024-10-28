using CorporateWebProject.Infrastructure.SmsService.Entitiy.Kobikom;
using CorporateWebProject.Application.Dto.Sms;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CorporateWebProject.Infrastructure.SmsService.Common
{
    public interface IKobikomService
    {
        public Balance SendMessage(KobikomJsonDTO model, string message, string number);
        public void SendMultipleSms(KobikomJsonDTO model, List<SingleUser> userList);
        public List<Title> GetTitles(KobikomJsonDTO model);
        public Balance GetBalance(KobikomJsonDTO model);

    }
}
