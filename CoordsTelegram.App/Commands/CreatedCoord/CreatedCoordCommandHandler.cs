using AutoMapper;
using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Commands.CreatedCoord
{
    internal class CreatedCoordCommandHandler : IRequestHandler<CreatedCoordCommand, CreatedCoordCommandResult>
    {
        private readonly ITelegramChatService _telegramChatService;

        public CreatedCoordCommandHandler(ITelegramChatService telegramChatService)
        {
            _telegramChatService = telegramChatService;
        }

        public async Task<CreatedCoordCommandResult> Handle(CreatedCoordCommand request, CancellationToken cancellationToken)
        {
            if (request.Data != null)
            {
                await _telegramChatService.SendToChannels(request.Data);
            }

            return new CreatedCoordCommandResult();
        }
    }
}
