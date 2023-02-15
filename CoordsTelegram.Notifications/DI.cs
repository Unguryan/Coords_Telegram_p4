using CoordsTelegram.App.Services;
using CoordsTelegram.Notifications.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoordsTelegram.Notifications
{
    public static class DI
    {
        public static void AddNotifications(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}
