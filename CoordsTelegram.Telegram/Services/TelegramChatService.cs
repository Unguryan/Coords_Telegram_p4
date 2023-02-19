using CoordsTelegram.App.Repositories;
using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.ViewModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CoordsTelegram.TelegramBot.Services
{
    public class TelegramChatService : ITelegramChatService
    {
        private readonly ITelegramChatRepository _repository;
        private readonly ITelegramBotClient _botClient;

        public TelegramChatService(ITelegramChatRepository repository,
                                   ITelegramBotClient botClient)
        {
            _repository = repository;
            _botClient = botClient;
        }

        public async Task<bool> AddAdmin(string idChat)
        {
            return await _repository.AddAdmin(idChat);
        }

        public async Task<bool> AddChannel(string idChannel, string idUser)
        {
            if (await _repository.IsUserAdmin(idUser))
            {
                return await _repository.AddChannel(idChannel);
            }

            return false;
        }

        public async Task<bool> RemoveAdmin(string idChat)
        {
            return await _repository.RemoveAdmin(idChat);
        }

        public async Task<bool> RemoveChannel(string idChannel, string idUser)
        {
            if (await _repository.IsUserAdmin(idUser))
            {
                return await _repository.RemoveChannel(idChannel);
            }

            return false;
        }

        public async Task SendToChannels(CoordDetailsViewModel data)
        {
            var message = data.GetTelegramMessage();

            var channels = await _repository.GetChannels();

            foreach (var channel in channels)
            {
                var chatId = new Chat()
                {
                    Id = long.Parse(channel)
                };

                var location = await _botClient.SendLocationAsync(chatId, (double)data.Latitude, (double)data.Longitude);

                if (string.IsNullOrEmpty(message))
                {
                    return;
                }

                await _botClient.SendTextMessageAsync(chatId, message, replyToMessageId: location.MessageId);
            }
        }
    }
}
