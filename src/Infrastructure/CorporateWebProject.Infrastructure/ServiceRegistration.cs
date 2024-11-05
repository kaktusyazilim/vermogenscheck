using CorporateWebProject.Infrastructure.Notification.Abstract;
using CorporateWebProject.Infrastructure.Notification.Concrete;
using CorporateWebProject.Infrastructure.Payment.Abstract;
using CorporateWebProject.Infrastructure.Payment.Concrete;
using CorporateWebProject.Infrastructure.SmsService.Common;
using CorporateWebProject.Infrastructure.SmsService.Concrete;
using CorporateWebProject.Infrastructure.SmsService.Handler;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IKobikomService, KobikomService>();
            services.AddScoped<INetgsmService, NetgsmService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IBaseSmsService, BaseSmsService>();
            services.AddScoped<IIyzicoService, IyzicoService>();
        }
    }
}
