using AutoMapper;
using CoordsTelegram.App.Repositories;
using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.Models;
using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAuthLinkGenerator _authLinkGenerator;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository, IAuthLinkGenerator authLinkGenerator, IMapper mapper)
        {
            _authRepository = authRepository;
            _authLinkGenerator = authLinkGenerator;
            _mapper = mapper;
        }

        public async Task<CreateAuthLinkViewModel> CreateAuthLinkAsync(CancellationToken cancellationToken = default)
        {
            var authViewModel = await _authLinkGenerator.Generate();
            var result = await _authRepository.AddNewKeyAsync(authViewModel.Key);

            if (!result)
            {
                return new CreateAuthLinkViewModel(string.Empty, string.Empty);
            }

            return authViewModel;
        }

        public async Task<AuthLink?> GetAuthLinkByChatIdAsync(string chatId)
        {
            var authLinks = (await _authRepository.GetAuthLinksByChatIdAsync(chatId));

            var linkToRevome = authLinks.Where(x => x.IsExpired);
            if (authLinks != null && authLinks.Any())
            {
                await _authRepository.RemoveRangeByKeyAsync(linkToRevome.Select(x => x.Key).ToList());
            }

            var remainedLinks = authLinks?.Except(linkToRevome);

            var lastAdded = remainedLinks?.FirstOrDefault(x => x.Expired == remainedLinks.Max(x => x.Expired));

            return lastAdded;
        }

        public async Task<AuthLink?> GetAuthLinkByKeyAsync(string key)
        {
            return await _authRepository.GetAuthLinkByKeyAsync(key);
        }

        public async Task<bool> RemoveLinkAsync(string key)
        {
            return await _authRepository.RemoveByKeyAsync(key);
        }

        public async Task<bool> UpdateChatIdAuthLinkAsync(string key, string chatId)
        {
            //remove oldest 
            var authLinks = (await _authRepository.GetAuthLinksByChatIdAsync(chatId)).Where(x => x.Key != key);

            if(authLinks != null && authLinks.Any())
            {
                await _authRepository.RemoveRangeByKeyAsync(authLinks.Select(x => x.Key).ToList());
            }

            return await _authRepository.ChangeByKeyAsync(key, chatId);
        }

        public async Task<bool> UpdateUserAuthLinkAsync(string key, TelegramUser user)
        {
            var authLink = _authRepository.GetAuthLinkByKeyAsync(key);

            if (authLink == null)
            {
                return false;   
            }

            return await _authRepository.ChangeByKeyAsync(key, _mapper.Map<TelegramUserInfoViewModel>(user));
        }
    }
}
