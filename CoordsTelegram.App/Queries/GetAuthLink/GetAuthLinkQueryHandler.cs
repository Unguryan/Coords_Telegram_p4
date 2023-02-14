using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Queries.GetAuthLink
{
    public class GetAuthLinkQueryHandler : IRequestHandler<GetAuthLinkQuery, GetAuthLinkQueryResult>
    {
        private readonly IAuthService _authService;

        public GetAuthLinkQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GetAuthLinkQueryResult> Handle(GetAuthLinkQuery request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetAuthLinkByKeyAsync(request.Key);

            return new GetAuthLinkQueryResult(result != null, result);
        }
    }
}
