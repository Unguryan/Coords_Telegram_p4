using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.ViewModels;
using CoordsTelegram.Notifications.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CoordsTelegram.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotifyUsersHub> _hub;
        private readonly ILogger _logger;

        public NotificationService(IHubContext<NotifyUsersHub> hub, ILogger<NotificationService> logger)
        {
            _hub = hub;
            _logger = logger;
        }

        public async Task<bool> SendSuccessLoginNotification(GetTokenInfoViewModel token)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(token);
                await _hub.Clients.All.SendAsync("ReceiveLoginNotification", jsonData);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }

            return true;
        }
    }
}
