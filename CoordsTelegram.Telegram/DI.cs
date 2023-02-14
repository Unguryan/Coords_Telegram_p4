using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoordsTelegram.Telegram.Services;
using CoordsTelegram.Telegram.Services.Interfaces;
using CoordsTelegram.TelegramBot.Services;
using Telegram.Bot;
using CoordsTelegram.Domain.Options;
using Microsoft.Extensions.Options;

namespace CoordsTelegram.TelegramBot
{
    public static class DI
    {
        public static void AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    var botConfig = sp.GetService<IOptions<TelegramBotOptions>>();
                    TelegramBotClientOptions options = new(botConfig.Value.BotToken);
                    return new TelegramBotClient(options, httpClient);
                });

            services.AddScoped<ITelegramMessageService, TelegramMessageService>();
            services.AddScoped<UpdateHandler>();

            services.AddHostedService<ConfigureWebhook>();

            services.AddControllersWithViews().AddNewtonsoftJson();

            
        }
    }
}
