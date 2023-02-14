using CoordsTelegram.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace CoordsTelegram.Infrastructure.Services
{
    public class AuthLinkBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public AuthLinkBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<AuthLinkBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var timeout = 60000 - DateTime.Now.Second * 1000;
                await Task.Delay(TimeSpan.FromSeconds(timeout), stoppingToken);

                try
                {
                    // Create new IServiceScope on each iteration.
                    // This way we can leverage benefits of Scoped TReceiverService
                    // and typed HttpClient - we'll grab "fresh" instance each time
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var cleanerService = scope.ServiceProvider.GetRequiredService<IAuthLinkCreanerService>();
                        var count = await cleanerService.CleanExpiredLinksAsync();
                        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}: Removed {count} links.");
                    }
                    //var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
                    //var propsService = scope.ServiceProvider.GetRequiredService<IPropolsalsService>();

                    //var res = await propsService.GetUpdatedPropolsals();

                    ////360607028
                    //var testId = new ChatId(long.Parse("360607028"));
                    //if (res != null && res.Any())
                    //{
                    //    foreach (var item in res)
                    //    {
                    //        await botClient.SendTextMessageAsync(testId,
                    //                                             item.GetNotificationMessage(),
                    //                                             ParseMode.Html,
                    //                                             disableWebPagePreview: true,
                    //                                             replyMarkup: item.GetInlineKeyboard());
                    //    }
                    //}
                }

                catch (Exception ex)
                {
                    _logger.LogError("Auth worker failed with exception: {Exception}", ex);

                    // Cooldown if something goes wrong
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }
}
