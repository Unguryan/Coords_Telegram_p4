using CoordsTelegram.Domain.Models;
using MediatR;

namespace CoordsTelegram.App.Commands.AddUserToAuthLink
{
    public record AddUserToAuthLinkCommand(string Key, TelegramUser? TelegramUser) : IRequest<AddUserToAuthLinkCommandResult>;
}
