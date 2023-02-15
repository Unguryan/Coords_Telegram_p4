using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.App.Services
{
    public interface ITelegramUserService
    {
        Task<AddedTelegramUserViewModel> AddTelegramUserAsync(CreateTelegramUserViewModel createTelegramUserViewModel);
        Task<TelegramUser?> GetTelegramUserByChatIdAsync(string chatId);
    }
}
