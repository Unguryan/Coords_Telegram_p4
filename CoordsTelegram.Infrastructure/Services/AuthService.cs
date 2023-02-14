using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAuthLinkGenerator _authLinkGenerator;

        public AuthService(IAuthRepository authRepository, IAuthLinkGenerator authLinkGenerator)
        {
            _authRepository = authRepository;
            _authLinkGenerator = authLinkGenerator;
        }

        public async Task<CreateAuthLinkViewModel> CreateAuthLinkAsync(CancellationToken cancellationToken = default)
        {
            var authViewModel = await _authLinkGenerator.Generate();
            var result = await _authRepository.AddNewKey(authViewModel.Key);

            if (!result)
            {
                return new CreateAuthLinkViewModel(string.Empty, string.Empty);
            }

            return authViewModel;
        }

        public async Task<AuthLink?> GetAuthLinkByChatIdAsync(string chatId)
        {
            var authLinks = (await _authRepository.GetAuthLinksByChatId(chatId));

            var linkToRevome = authLinks.Where(x => x.IsExpired);
            if (authLinks != null && authLinks.Any())
            {
                await _authRepository.RemoveRangeByKey(linkToRevome.Select(x => x.Key).ToList());
            }

            var remainedLinks = authLinks?.Except(linkToRevome);

            var lastAdded = remainedLinks?.FirstOrDefault(x => x.Expired == remainedLinks.Max(x => x.Expired));

            return lastAdded;
        }

        public async Task<AuthLink?> GetAuthLinkByKeyAsync(string key)
        {
            return await _authRepository.GetAuthLinkByKey(key);
        }

        public async Task<bool> UpdateChatIdAuthLinkAsync(string key, string chatId)
        {
            //AddLogicHERE -> Need to have only one activeLink
            var authLinks = (await _authRepository.GetAuthLinksByChatId(chatId)).Where(x => x.Key != key);

            if(authLinks != null && authLinks.Any())
            {
                await _authRepository.RemoveRangeByKey(authLinks.Select(x => x.Key).ToList());
            }

            return await _authRepository.ChangeByKey(key, chatId);
        }
    }
}
