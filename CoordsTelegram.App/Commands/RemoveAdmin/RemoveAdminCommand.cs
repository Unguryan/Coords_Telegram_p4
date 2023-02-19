using MediatR;

namespace CoordsTelegram.App.Commands.RemoveAdmin
{
    public record RemoveAdminCommand(string IdAdmin) : IRequest<RemoveAdminCommandResult>;
}
