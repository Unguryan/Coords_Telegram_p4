using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.App.Repositories
{
    public interface ITokenRepository
    {
        Task<TokenInfoViewModel> GetTokenInfoAsync(string key);

        Task<bool> AddTokenAsync(AddTokenViewModel request, DateTime Expired);

        Task<List<TokenInfoViewModel>> GetTokensAsync();

        Task<List<TokenInfoViewModel>> GetTokensByChatIdAsync(string chatId);

        Task<bool> RemoveByKeyAsync(string key);

        Task<int> RemoveRangeByKeyAsync(List<string> key);
    }
}
