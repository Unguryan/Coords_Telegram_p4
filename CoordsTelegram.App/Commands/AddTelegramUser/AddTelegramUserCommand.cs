using MediatR;

namespace CoordsTelegram.App.Commands.AddTelegramUser
{
    public record AddTelegramUserCommand(string ChatId,
                                         string PhoneNumber,
                                         string FullName,
                                         string UserName) : IRequest<AddTelegramUserCommandResult>;
    
}
