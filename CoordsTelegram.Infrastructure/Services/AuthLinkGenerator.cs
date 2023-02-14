using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.Options;
using CoordsTelegram.Domain.ViewModels;
using Microsoft.Extensions.Options;

namespace CoordsTelegram.Infrastructure.Services
{
    public class AuthLinkGenerator : IAuthLinkGenerator
    {
        private const string BaseUrl = "https://t.me/{0}?start=auth={1}";


        private readonly IOptions<TelegramBotOptions> _options;
        public AuthLinkGenerator(IOptions<TelegramBotOptions> options)
        {
            _options = options;
        }

        public async Task<CreateAuthLinkViewModel> Generate()
        {
            var guid = Guid.NewGuid().ToString();
            var url = string.Format(BaseUrl, _options.Value.BotName, guid);
            return new CreateAuthLinkViewModel(url, guid);
        }
    }
}
