namespace CoordsTelegram.App.Services
{
    public interface INotificationService
    {
        Task<bool> SendSuccessLoginNotification(string key);
    }
}
