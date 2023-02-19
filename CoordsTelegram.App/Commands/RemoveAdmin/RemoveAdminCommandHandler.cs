using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Commands.RemoveAdmin
{
    internal class RemoveAdminCommandHandler : IRequestHandler<RemoveAdminCommand, RemoveAdminCommandResult>
    {
        private readonly ITelegramChatService _telegramChatService;

        public RemoveAdminCommandHandler(ITelegramChatService telegramChatService)
        {
            _telegramChatService = telegramChatService;
        }

        public async Task<RemoveAdminCommandResult> Handle(RemoveAdminCommand request, CancellationToken cancellationToken)
        {
            var result = await _telegramChatService.RemoveAdmin(request.IdAdmin);

            return new RemoveAdminCommandResult(result);
        }
    }
}
