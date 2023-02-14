using MediatR;

namespace CoordsTelegram.App.Commands.CreateAuthLink
{
    public record CreateAuthLinkCommand() : IRequest<CreateAuthLinkCommandResult>;
}
