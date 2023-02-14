using AutoMapper;
using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Commands.SendLoginNotification
{
    public class SendLoginNotificationCommandHandler : IRequestHandler<SendLoginNotificationCommand, SendLoginNotificationCommandResult>
    {
        private readonly IAuthService _authService;
        private readonly INotificationService _notificationService;

        public SendLoginNotificationCommandHandler(IAuthService authService/*, INotificationService notificationService*/)
        {
            _authService = authService;
            //_notificationService = notificationService;
        }

        public async Task<SendLoginNotificationCommandResult> Handle(SendLoginNotificationCommand request, CancellationToken cancellationToken)
        {
            var authLink = await _authService.GetAuthLinkByKeyAsync(request.Key);

            if(authLink == null ||
               string.IsNullOrEmpty(authLink.ChatId))
            {
                return new SendLoginNotificationCommandResult(false, "Internal Server Error.");
            }

            //var result = await _notificationService.SendSuccessLoginNotification(authLink);

            return new SendLoginNotificationCommandResult(true);
        }
    }
}
