using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoordsTelegram.App;
using CoordsTelegram.Infrastructure.Services;
using CoordsTelegram.App.Services;

namespace CoordsTelegram.Infrastructure
{
    public static class DI
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAssembly(configuration);

            services.AddScoped<IAuthLinkCreanerService, AuthLinkCreanerService>();
            services.AddScoped<ITokenCreanerService, TokenCreanerService>();
            services.AddScoped<IAuthLinkGenerator, AuthLinkGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITelegramUserService, TelegramUserService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddHostedService<AuthLinkBackgroundService>();
            services.AddHostedService<TokenBackgroundService>();
        }
    }
}
