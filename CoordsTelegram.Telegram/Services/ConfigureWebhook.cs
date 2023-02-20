using CoordsTelegram.Domain.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Telegram.Bot;

namespace CoordsTelegram.TelegramBot.Services
{
    public class ConfigureWebhook : IHostedService
    {
        private readonly ILogger<ConfigureWebhook> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TelegramBotOptions _botConfig;

        public ConfigureWebhook(
            ILogger<ConfigureWebhook> logger,
            IServiceProvider serviceProvider,
            IOptions<TelegramBotOptions> botOptions)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _botConfig = botOptions.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            //for docker coords_telegram_p4-ngrok-1

            var webhookAddress = "";

            using (var http = new HttpClient())
            {
                //
                var resp = await http.GetAsync("http://coords_telegram_p4-ngrok-1:4040/api/tunnels");
                //var resp = await http.GetAsync("http://coords_telegram_p4-ngrok-1:4551/api/tunnels");
                //var resp = await http.GetAsync("http://coords_telegram_p4-ngrok-1:4040/api/tunnels");
                //var resp = await http.GetAsync("http://172.21.0.5:4040/api/tunnels");

                resp.EnsureSuccessStatusCode();

                var resp_str = await resp.Content.ReadAsStringAsync();

                Console.WriteLine("TEST DATA: " + resp_str);

                var jObj = JObject.Parse(resp_str);
                var jArr = JArray.Parse(jObj.SelectToken("$.tunnels").ToString());

                var jTunnelObj = jArr[0];

                var public_urlObj = jTunnelObj.SelectToken("$.public_url").ToString();

                Console.WriteLine("URL: " + public_urlObj);

                webhookAddress = public_urlObj + _botConfig.Route;
            }

            //var webhookAddress = $"{_botConfig.HostAddress}{_botConfig.Route}";
            _logger.LogInformation("Setting webhook: {WebhookAddress}", webhookAddress);
            await botClient.SetWebhookAsync(
                url: webhookAddress,
                cancellationToken: cancellationToken);

            //for default - without docker
            //var webhookAddress = $"{_botConfig.HostAddress}{_botConfig.Route}";
            //_logger.LogInformation("Setting webhook: {WebhookAddress}", webhookAddress);
            //await botClient.SetWebhookAsync(
            //    url: webhookAddress,
            //    cancellationToken: cancellationToken);
            
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            // Remove webhook on app shutdown
            _logger.LogInformation("Removing webhook");
            await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
        }
    }
}
