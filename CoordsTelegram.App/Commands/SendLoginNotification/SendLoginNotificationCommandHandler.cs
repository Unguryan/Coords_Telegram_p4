using AutoMapper;
using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.ViewModels;
using MediatR;

namespace CoordsTelegram.App.Commands.SendLoginNotification
{
    public class SendLoginNotificationCommandHandler : IRequestHandler<SendLoginNotificationCommand, SendLoginNotificationCommandResult>
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public SendLoginNotificationCommandHandler(IAuthService authService, 
                                                   ITokenService tokenService,
                                                   IMapper mapper,
                                                   INotificationService notificationService)
        {
            _authService = authService;
            _tokenService = tokenService;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<SendLoginNotificationCommandResult> Handle(SendLoginNotificationCommand request, CancellationToken cancellationToken)
        {
            var authLink = await _authService.GetAuthLinkByKeyAsync(request.Key);

            if(authLink == null ||
               string.IsNullOrEmpty(authLink.ChatId))
            {
                return new SendLoginNotificationCommandResult(false, "Internal Server Error.");
            }

            var isAdded = await _tokenService.SetTokenInfoByKeyAsync(_mapper.Map<AddTokenViewModel>(authLink));
            if (!isAdded)
            {
                return new SendLoginNotificationCommandResult(false, "Internal Server Error.");
            }

            await _authService.RemoveLinkAsync(authLink.Key);

            var token = await _tokenService.GetTokenInfoByKeyAsync(request.Key);
            if(token == null)
            {
                return new SendLoginNotificationCommandResult(false, "Internal Server Error.");
            }

            var result = await _notificationService.SendSuccessLoginNotification(token);

            return new SendLoginNotificationCommandResult(result);
        }
    }
}
