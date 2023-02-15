using CoordsTelegram.App.Repositories;
using CoordsTelegram.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordsTelegram.Infrastructure.Services
{
    public class TokenCreanerService : ITokenCreanerService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenCreanerService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<int> CleanExpiredTokensAsync()
        {
            var links = await _tokenRepository.GetTokensAsync();

            var linksToRemove = links.Where(l => l.IsExpired)
                                     .Select(l => l.Key).ToList();

            return await _tokenRepository.RemoveRangeByKeyAsync(linksToRemove);
        }
    }
}
