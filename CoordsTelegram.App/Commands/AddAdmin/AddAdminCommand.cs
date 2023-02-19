using MediatR;

namespace CoordsTelegram.App.Commands.AddAdmin
{
    public record AddAdminCommand(string IdAdmin) : IRequest<AddAdminCommandResult>;
}
