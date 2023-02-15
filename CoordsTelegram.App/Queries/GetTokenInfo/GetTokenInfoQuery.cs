using MediatR;

namespace CoordsTelegram.App.Queries.GetTokenInfo
{
    public record GetTokenInfoQuery(string Key) : IRequest<GetTokenInfoQueryResult>;
}
