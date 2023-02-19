using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.App.Services
{
    public interface ITelegramChatService
    {
        Task SendToChannels(CoordDetailsViewModel data);

        Task<bool> AddChannel(string idChannel, string idUser);

        Task<bool> RemoveChannel(string idChannel, string idUser);

        Task<bool> AddAdmin(string idChat);

        Task<bool> RemoveAdmin(string idChat);

    }
}
