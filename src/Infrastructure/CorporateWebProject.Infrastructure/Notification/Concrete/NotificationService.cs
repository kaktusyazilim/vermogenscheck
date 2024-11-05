using CorporateWebProject.Application.ViewModels.Notification;
using CorporateWebProject.Infrastructure.Notification.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.Notification.Concrete
{
    public class NotificationService : INotificationService
    {
        public FirebaseResultVM SendNotification(string title, string message, string image, string to,string senderId,string key,NotificationVM notification)
        {
            try
            {
                FirebaseResultVM result = new();
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", key));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentType = "application/json";
                var payload = new
                {
                    to = to,
                    notification = new
                    {
                        title = title,
                        body = message,
                        icon = "ic_launcher",
                        sound = "default",
                        image = image,
                        main_picture = image
                    },
                    data = new
                    {
                        model = JsonConvert.SerializeObject(notification, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                        main_picture = image
                    }
                };
                string postbody = JsonConvert.SerializeObject(payload).ToString().Replace("image_url", "image-url");
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();

                                    result = JsonConvert.DeserializeObject<FirebaseResultVM>(sResponseFromServer)!;
                                }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return new();
            }
        }
    }
}
