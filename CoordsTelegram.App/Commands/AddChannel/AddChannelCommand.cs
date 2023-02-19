using MediatR;

namespace CoordsTelegram.App.Commands.AddChannel
{
    public record AddChannelCommand(string IdChannel, string IdUser) : IRequest<Unit>;
}
