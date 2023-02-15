using CoordsTelegram.App.Repositories;
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


        public async Task<AddedTelegramUserViewModel> AddTelegramUserAsync(CreateTelegramUserViewModel createTelegramUserViewModel)
        {
            var res = await _userRepository.AddUserAsync(createTelegramUserViewModel);
            if (!res)
            {
                return new AddedTelegramUserViewModel(false, null);
            }

            var user = await _userRepository.GetUserAsync(createTelegramUserViewModel.ChatId);
            return new AddedTelegramUserViewModel(user != null, user);
        }

        public async Task<TelegramUser?> GetTelegramUserByChatIdAsync(string chatId)
        {
            return await _userRepository.GetUserAsync(chatId);
        }
    }
}
