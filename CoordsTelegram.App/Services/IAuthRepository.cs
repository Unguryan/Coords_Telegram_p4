using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.App.Services
{
    public interface IAuthRepository
    {
        Task<List<AuthLink>> GetAuthLinks();

        Task<List<AuthLink>> GetAuthLinksByChatId(string chatId);

        Task<AuthLink> GetAuthLinkByKey(string key);

        Task<bool> AddNewKey(string key);

        Task<bool> RemoveByKey(string key);

        Task<int> RemoveRangeByKey(List<string> key);

        Task<bool> ChangeByKey(string key, TelegramUserInfoViewModel infoViewModel); 

        Task<bool> ChangeByKey(string key, string ChatId);
    }
}
