using CoordsTelegram.Domain.Models;

namespace CoordsTelegram.Domain.ViewModels
{
    public record AddedTelegramUserViewModel(bool IsAdded, TelegramUser? User);
}
