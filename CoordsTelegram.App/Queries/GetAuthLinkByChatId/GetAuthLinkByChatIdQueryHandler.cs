using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Queries.GetAuthLinkByChatId
{
    public class GetAuthLinkByChatIdQueryHandler : IRequestHandler<GetAuthLinkByChatIdQuery, GetAuthLinkByChatIdQueryResult>
    {
        private readonly IAuthService _authService;

        public GetAuthLinkByChatIdQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GetAuthLinkByChatIdQueryResult> Handle(GetAuthLinkByChatIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetAuthLinkByChatIdAsync(request.ChatId);

            return new GetAuthLinkByChatIdQueryResult(result != null, result);
        }
    }
}
