using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.Infrastructure.Services
{
    public class TelegramUserService : ITelegramUserService
    {
        private readonly ITelegramUserRepository _userRepository;

        public TelegramUserService(ITelegramUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<bool> AddTelegramUserAsync(CreateTelegramUserViewModel createTelegramUserViewModel)
        {
            return await _userRepository.AddUserAsync(createTelegramUserViewModel);
        }

        public async Task<TelegramUser?> GetTelegramUserByChatIdAsync(string chatId)
        {
            return await _userRepository.GetUserAsync(chatId);
        }
    }
}
