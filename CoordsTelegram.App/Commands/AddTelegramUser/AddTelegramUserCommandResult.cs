namespace CoordsTelegram.App.Commands.AddTelegramUser
{
    public record AddTelegramUserCommandResult(bool IsAdded, string? ErrorMessage = null);
}
