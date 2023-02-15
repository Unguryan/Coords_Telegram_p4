using AutoMapper;
using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Queries.GetTokenInfo
{
    internal class GetTokenInfoQueryHandler : IRequestHandler<GetTokenInfoQuery, GetTokenInfoQueryResult>
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public GetTokenInfoQueryHandler(ITokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<GetTokenInfoQueryResult> Handle(GetTokenInfoQuery request, CancellationToken cancellationToken)
        {
            var result = await _tokenService.GetTokenInfoByKeyAsync(request.Key);

            return _mapper.Map<GetTokenInfoQueryResult>(result);
        }
    }
}
