using MediatR;

namespace CoordsTelegram.App.Queries.GetAuthLink
{
    public record GetAuthLinkQuery(string Key) : IRequest<GetAuthLinkQueryResult>;
}
