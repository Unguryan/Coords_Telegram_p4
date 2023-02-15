using CoordsTelegram.Domain.Models;

namespace CoordsTelegram.App.Commands.AddTelegramUser
{
    public record AddTelegramUserCommandResult(bool IsAdded, TelegramUser? User, string? ErrorMessage = null);
}
