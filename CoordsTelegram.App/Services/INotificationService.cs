using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.App.Services
{
    public interface INotificationService
    {
        Task<bool> SendSuccessLoginNotification(GetTokenInfoViewModel token);
    }
}
