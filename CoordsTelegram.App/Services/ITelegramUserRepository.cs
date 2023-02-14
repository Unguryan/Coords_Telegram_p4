using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.App.Services
{
    public interface ITelegramUserRepository
    {
        Task<TelegramUser?> GetUserAsync(string id);

        Task<bool> AddUserAsync(CreateTelegramUserViewModel createTelegramUserViewModel);
    }
}
