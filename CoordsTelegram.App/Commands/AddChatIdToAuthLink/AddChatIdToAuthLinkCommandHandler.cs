using CoordsTelegram.App.Services;
using FluentValidation;
using MediatR;

namespace CoordsTelegram.App.Commands.AddChatIdToAuthLink
{
    public class AddChatIdToAuthLinkCommandHandler : IRequestHandler<AddChatIdToAuthLinkCommand, AddChatIdToAuthLinkCommandResult>
    {
        private readonly IAuthService _authService;
        private readonly IValidator<AddChatIdToAuthLinkCommand> _validator;

        public AddChatIdToAuthLinkCommandHandler(IAuthService authService, IValidator<AddChatIdToAuthLinkCommand> validator)
        {
            _authService = authService;
            _validator = validator;
        }

        public async Task<AddChatIdToAuthLinkCommandResult> Handle(AddChatIdToAuthLinkCommand request, CancellationToken cancellationToken)
        {
            var validateRes = await _validator.ValidateAsync(request);

            if (!validateRes.IsValid)
            {
                var errorsStr = string.Empty;
                validateRes.Errors.ForEach(e => errorsStr += $"{e}\n");
                return new AddChatIdToAuthLinkCommandResult(false, errorsStr);
            }

            var result = await _authService.UpdateChatIdAuthLinkAsync(request.Key, request.ChatId);

            return new AddChatIdToAuthLinkCommandResult(result);
        }
    }
}
