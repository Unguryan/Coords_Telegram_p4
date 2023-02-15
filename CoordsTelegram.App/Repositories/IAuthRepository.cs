using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.App.Repositories
{
    public interface IAuthRepository
    {
        Task<List<AuthLink>> GetAuthLinksAsync();

        Task<List<AuthLink>> GetAuthLinksByChatIdAsync(string chatId);

        Task<AuthLink> GetAuthLinkByKeyAsync(string key);

        Task<bool> AddNewKeyAsync(string key);

        Task<bool> RemoveByKeyAsync(string key);

        Task<int> RemoveRangeByKeyAsync(List<string> key);

        Task<bool> ChangeByKeyAsync(string key, TelegramUserInfoViewModel infoViewModel);

        Task<bool> ChangeByKeyAsync(string key, string ChatId);
    }
}
