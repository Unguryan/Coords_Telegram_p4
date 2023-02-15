using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.App.Services
{
    public interface ITokenService
    {
        Task<List<TokenInfoViewModel>> GetTokensAsync();

        Task<GetTokenInfoViewModel> GetTokenInfoByKeyAsync(string key);

        Task<bool> SetTokenInfoByKeyAsync(AddTokenViewModel request);

        Task<bool> RemoveByKeyAsync(string key);

        Task<int> RemoveRangeByKeyAsync(List<string> key);
    }
}
