using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Commands.AddAdmin
{
    public class AddAdminCommandHandler : IRequestHandler<AddAdminCommand, AddAdminCommandResult>
    {
        private readonly ITelegramChatService _telegramChatService;

        public AddAdminCommandHandler(ITelegramChatService telegramChatService)
        {
            _telegramChatService = telegramChatService;
        }

        public async Task<AddAdminCommandResult> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {
            var result = await _telegramChatService.AddAdmin(request.IdAdmin);

            return new AddAdminCommandResult(result);
        }
    }
}

