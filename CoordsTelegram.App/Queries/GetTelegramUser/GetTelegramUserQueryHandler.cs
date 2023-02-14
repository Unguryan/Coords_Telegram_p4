using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Queries.GetTelegramUser
{
    public class GetTelegramUserQueryHandler : IRequestHandler<GetTelegramUserQuery, GetTelegramUserQueryResult>
    {
        private readonly ITelegramUserService _telegramUserService;

        public GetTelegramUserQueryHandler(ITelegramUserService telegramUserService)
        {
            _telegramUserService = telegramUserService;
        }

        public async Task<GetTelegramUserQueryResult> Handle(GetTelegramUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _telegramUserService.GetTelegramUserByChatIdAsync(request.Id);

            return new GetTelegramUserQueryResult(result != null, result);
        }
    }
}
