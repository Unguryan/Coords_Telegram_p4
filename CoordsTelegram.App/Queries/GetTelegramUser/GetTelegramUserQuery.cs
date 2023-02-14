using MediatR;

namespace CoordsTelegram.App.Queries.GetTelegramUser
{
    public record GetTelegramUserQuery(string Id) : IRequest<GetTelegramUserQueryResult>;
}
