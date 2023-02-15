using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        public async Task<bool> SendSuccessLoginNotification(GetTokenInfoViewModel token)
        {
            return true;
        }
    }
}
