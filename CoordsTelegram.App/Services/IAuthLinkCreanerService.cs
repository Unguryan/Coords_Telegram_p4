namespace CoordsTelegram.App.Services
{
    public interface IAuthLinkCreanerService
    {
        Task<int> CleanExpiredLinksAsync();
    }
}
