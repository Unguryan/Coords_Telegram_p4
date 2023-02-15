using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.App.Services
{
    public interface IAuthService
    {
        Task<CreateAuthLinkViewModel> CreateAuthLinkAsync(CancellationToken cancellationToken = default);
        Task<AuthLink?> GetAuthLinkByKeyAsync(string key);
        Task<AuthLink?> GetAuthLinkByChatIdAsync(string chatId);
        Task<bool> UpdateChatIdAuthLinkAsync(string key, string chatId);
        Task<bool> UpdateUserAuthLinkAsync(string key, TelegramUser telegramUser);
        Task<bool> RemoveLinkAsync(string key);
    }
}
