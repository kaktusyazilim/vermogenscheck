using CorporateWebProject.Application.Dto.Sms;
using CorporateWebProject.Infrastructure.SmsService.Common;
using CorporateWebProject.Infrastructure.SmsService.Entitiy.Kobikom;
using CorporateWebProject.Application.Dto.Sms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.SmsService.Concrete
{
    public class KobikomService : IKobikomService
    {
        public Balance SendMessage(KobikomJsonDTO model, string message, string number)
        {
            try
            {
                var title = GetTitles(model);
                string URL = "https://smspaneli.kobikom.com.tr/sms/api?action=send-sms&api_key=" + model.ApiKey + "&to=" + number + "&from=" + title[0].sender_id + "&sms=" + message + "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json; charset=utf-8";
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("username:password"));
                request.PreAuthenticate = true;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream responseStream = response!.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    Balance reseliazeBalance = JsonConvert.DeserializeObject<Balance>(reader.ReadToEnd())!;
                    return reseliazeBalance;
                }
            }
            catch (Exception)
            {
                return null!;
            }
        }


        public void SendMultipleSms(KobikomJsonDTO model, List<SingleUser> userList)
        {
            try
            {
                var title = GetTitles(model);
                string webAddr = "https://smspaneli.kobikom.com.tr/sms/api/multiple?action=send-sms&api_key=" + model.ApiKey + "&response=json";
                var httpWebRequest = WebRequest.CreateHttp(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var json = JsonConvert.SerializeObject(userList);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    streamReader.ReadToEnd();
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Title> GetTitles(KobikomJsonDTO model)
        {
            try
            {
                string URL = "https://smspaneli.kobikom.com.tr/sms/api/sender-id?action=list&api_key=" + model.ApiKey + "&response=json";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json; charset=utf-8";
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("username:password"));
                request.PreAuthenticate = true;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    List<Title> Titles = JsonConvert.DeserializeObject<List<Title>>(reader.ReadToEnd())!;
                    return Titles;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public Balance GetBalance(KobikomJsonDTO model)
        {
            try
            {
                string URL = "https://smspaneli.kobikom.com.tr/sms/api?action=check-balance&api_key=" + model.ApiKey + "&response=json";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json; charset=utf-8";
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("username:password"));
                request.PreAuthenticate = true;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    Balance reseliazeBalance = JsonConvert.DeserializeObject<Balance>(reader.ReadToEnd())!;
                    return reseliazeBalance;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
