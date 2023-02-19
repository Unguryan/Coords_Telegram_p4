using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CoordsTelegram.Telegram.Services.Interfaces
{
    public interface ITelegramMessageService
    {
        Task ReceiveMessage(Message message); 
        Task ReceiveCallback(CallbackQuery callbackQuery);
        Task ReceiveNewChannel(long channelId, long id, ChatMemberStatus status);
    }
}