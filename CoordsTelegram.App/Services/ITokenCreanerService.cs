namespace CoordsTelegram.App.Services
{
    public interface ITokenCreanerService
    {
        Task<int> CleanExpiredTokensAsync();
    }
}
