using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CoordsTelegram.TelegramBot
{
    public static class TelegramExtensions
    {
        public static ControllerActionEndpointConventionBuilder MapBotWebhookRoute<T>(
            this IEndpointRouteBuilder endpoints,
            string route)
        {
            var controllerName = typeof(T).Name.Replace("Controller", "");
            var actionName = typeof(T).GetMethods()[0].Name;

            return endpoints.MapControllerRoute(
                name: "bot_webhook",
                pattern: route,
                defaults: new { controller = controllerName, action = actionName });
        }
    }
}
