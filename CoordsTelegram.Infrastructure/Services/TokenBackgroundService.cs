using CoordsTelegram.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoordsTelegram.Infrastructure.Services
{
    internal class TokenBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public TokenBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<TokenBackgroundService> logger)
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
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var cleanerService = scope.ServiceProvider.GetRequiredService<ITokenCreanerService>();
                        var count = await cleanerService.CleanExpiredTokensAsync();
                        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}: Removed {count} TOKENS.");
                    }
                }

                catch (Exception ex)
                {
                    _logger.LogError("Token worker failed with exception: {Exception}", ex);

                    // Cooldown if something goes wrong
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }
}
