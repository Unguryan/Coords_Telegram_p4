using CoordsTelegram.App.Commands.RemoveChannel;
using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Commands.AddChannel
{
    public class AddChannelCommandHandler : IRequestHandler<AddChannelCommand>
    {
        private readonly ITelegramChatService _telegramChatService;

        public AddChannelCommandHandler(ITelegramChatService telegramChatService)
        {
            _telegramChatService = telegramChatService;
        }

        public async Task<Unit> Handle(AddChannelCommand request, CancellationToken cancellationToken)
        {
            await _telegramChatService.AddChannel(request.IdChannel, request.IdUser);
            return Unit.Value;
        }
    }
}
