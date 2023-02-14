using Telegram.Bot.Types;

namespace CoordsTelegram.Telegram.Services.Interfaces
{
    public interface ITelegramMessageService
    {
        Task ReceiveMessage(Message message); 
        Task ReceiveCallback(CallbackQuery callbackQuery); 
    }
}