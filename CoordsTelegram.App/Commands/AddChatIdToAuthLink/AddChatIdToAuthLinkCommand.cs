using MediatR;

namespace CoordsTelegram.App.Commands.AddChatIdToAuthLink
{
    public record AddChatIdToAuthLinkCommand(string Key, string ChatId) : IRequest<AddChatIdToAuthLinkCommandResult>;
}
