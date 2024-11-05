using CorporateWebProject.Application.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure.Notification.Abstract
{
    public interface INotificationService
    {
        public FirebaseResultVM SendNotification(string title, string message, string image, string to, string senderId, string key, NotificationVM model);
    }
}
