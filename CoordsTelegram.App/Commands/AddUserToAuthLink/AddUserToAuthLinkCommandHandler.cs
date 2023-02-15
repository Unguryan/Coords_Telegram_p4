using CoordsTelegram.App.Commands.AddChatIdToAuthLink;
using CoordsTelegram.App.Services;
using FluentValidation;
using MediatR;

namespace CoordsTelegram.App.Commands.AddUserToAuthLink
{
    public class AddUserToAuthLinkCommandHandler : IRequestHandler<AddUserToAuthLinkCommand, AddUserToAuthLinkCommandResult>
    {
        private readonly IAuthService _authService;

        public AddUserToAuthLinkCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AddUserToAuthLinkCommandResult> Handle(AddUserToAuthLinkCommand request, CancellationToken cancellationToken)
        {
            if(request.TelegramUser == null)
            {
                return new AddUserToAuthLinkCommandResult(false, "Internal Server Error.");
            }

            var result = await _authService.UpdateUserAuthLinkAsync(request.Key, request.TelegramUser);

            return new AddUserToAuthLinkCommandResult(result);
        }
    }
}
