using MediatR;

namespace CoordsTelegram.App.Queries.GetAuthLinkByChatId
{
    public record GetAuthLinkByChatIdQuery(string ChatId) : IRequest<GetAuthLinkByChatIdQueryResult>;
}
