using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Commands.RemoveChannel
{
    public class RemoveChannelCommandHandler : IRequestHandler<RemoveChannelCommand>
    {
        private readonly ITelegramChatService _telegramChatService;

        public RemoveChannelCommandHandler(ITelegramChatService telegramChatService)
        {
            _telegramChatService = telegramChatService;
        }

        public async Task<Unit> Handle(RemoveChannelCommand request, CancellationToken cancellationToken)
        {
            await _telegramChatService.RemoveChannel(request.IdChannel, request.IdUser);
            return Unit.Value;
        }
    }
}
