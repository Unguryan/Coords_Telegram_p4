namespace CoordsTelegram.App.Commands.SendLoginNotification
{
    public record SendLoginNotificationCommandResult(bool IsSent, string? ErrorMessage = null);
}
