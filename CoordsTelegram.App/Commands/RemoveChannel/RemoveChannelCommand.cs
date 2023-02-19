using MediatR;

namespace CoordsTelegram.App.Commands.RemoveChannel
{
    public record RemoveChannelCommand(string IdChannel, string IdUser) : IRequest<Unit>;
}
