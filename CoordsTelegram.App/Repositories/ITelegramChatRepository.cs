namespace CoordsTelegram.App.Repositories
{
    public interface ITelegramChatRepository
    {
        Task<bool> AddAdmin(string idChat);
        Task<bool> AddChannel(string idChannel);
        Task<List<string>> GetChannels();
        Task<bool> IsUserAdmin(string idUser);
        Task<bool> RemoveAdmin(string idChat);
        Task<bool> RemoveChannel(string idChannel);
    }
}
