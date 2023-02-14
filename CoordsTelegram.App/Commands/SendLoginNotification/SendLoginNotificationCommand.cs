using MediatR;

namespace CoordsTelegram.App.Commands.SendLoginNotification
{
    public record SendLoginNotificationCommand(string Key) : IRequest<SendLoginNotificationCommandResult>; 
}
