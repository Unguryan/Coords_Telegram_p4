using CoordsTelegram.App.Repositories;
using CoordsTelegram.EF_Core.Context;
using CoordsTelegram.EF_Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoordsTelegram.EF_Core
{
    public static class DI
    {
        public static void AddEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            var assebmly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assebmly);

            services.AddDbContext<AuthLinkContext>();
            services.AddDbContext<TelegramUserContext>();
            services.AddDbContext<TelegramChatContext>();
            services.AddDbContext<TokenContext>();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITelegramUserRepository, TelegramUserRepository>();
            services.AddScoped<ITelegramChatRepository, TelegramChatRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
        }
    }
}
