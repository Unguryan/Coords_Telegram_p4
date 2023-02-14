using CoordsTelegram.Domain.Models;

namespace CoordsTelegram.App.Queries.GetTelegramUser
{
    public record GetTelegramUserQueryResult(bool IsFound, TelegramUser? TelegramUser, string? ErrorMessage = null);
}
