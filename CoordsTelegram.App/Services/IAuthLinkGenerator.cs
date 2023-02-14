using CoordsTelegram.Domain.ViewModels;

namespace CoordsTelegram.App.Services
{
    public interface IAuthLinkGenerator
    {
        Task<CreateAuthLinkViewModel> Generate();
    }
}
