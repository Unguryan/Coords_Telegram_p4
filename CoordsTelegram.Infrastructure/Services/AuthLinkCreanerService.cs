using CoordsTelegram.App.Repositories;
using CoordsTelegram.App.Services;

namespace CoordsTelegram.Infrastructure.Services
{
    public class AuthLinkCreanerService : IAuthLinkCreanerService
    {
        private readonly IAuthRepository _authRepository;

        public AuthLinkCreanerService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<int> CleanExpiredLinksAsync()
        {
            var links = await _authRepository.GetAuthLinksAsync();

            var linksToRemove = links.Where(l => l.IsExpired && string.IsNullOrEmpty(l.ChatId))
                                         //(string.IsNullOrEmpty(l.PhoneNumber) || string.IsNullOrEmpty(l.FullName)))
                                     .Select(l => l.Key).ToList();

            var oldLinks = links.Where(l => DateTime.Now > l.Expired.AddMinutes(10)).Select(l => l.Key).ToList();
            linksToRemove.AddRange(oldLinks);

            return await _authRepository.RemoveRangeByKeyAsync(linksToRemove);
        }
    }
}
