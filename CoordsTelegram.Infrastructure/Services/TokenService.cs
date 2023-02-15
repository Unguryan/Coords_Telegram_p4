using CoordsTelegram.App.Repositories;
using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.ViewModels;
using System.Text;
using System.Text.Json;

namespace CoordsTelegram.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<GetTokenInfoViewModel> GetTokenInfoByKeyAsync(string key)
        {
            var res = await _tokenRepository.GetTokenInfoAsync(key);

            if (res == null)
            {
                return new GetTokenInfoViewModel(string.Empty, string.Empty, DateTime.Now);
            }

            var data = new TokenInfoToSendViewModel(res.ChatId, res.UserName, res.FullName, res.PhoneNumber);

            var dataJson = JsonSerializer.Serialize(data);
            var dataBytes = Encoding.UTF8.GetBytes(dataJson);
            var dataBase64 = Convert.ToBase64String(dataBytes);


            return new GetTokenInfoViewModel(key, dataBase64, res.Expired);
        }

        public async Task<List<TokenInfoViewModel>> GetTokensAsync()
        {
            return await _tokenRepository.GetTokensAsync();
        }

        public async Task<bool> RemoveByKeyAsync(string key)
        {
            return await _tokenRepository.RemoveByKeyAsync(key);
        }

        public async Task<int> RemoveRangeByKeyAsync(List<string> keys)
        {
            return await _tokenRepository.RemoveRangeByKeyAsync(keys);
        }

        public async Task<bool> SetTokenInfoByKeyAsync(AddTokenViewModel request)
        {
            return await _tokenRepository.AddTokenAsync(request, DateTime.Now.AddHours(1));
        }
    }
}

