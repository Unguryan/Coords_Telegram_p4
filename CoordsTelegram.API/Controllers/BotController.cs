using CoordsTelegram.Telegram.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace CoordsTelegram.API.Controllers
{
    public class BotController : ControllerBase
    {
        [HttpPost]
        //[ValidateTelegramBot]
        public async Task<IActionResult> Post(
             [FromBody] Update update,
             [FromServices] UpdateHandler handleUpdateService,
             CancellationToken cancellationToken)
        {
            await handleUpdateService.HandleUpdateAsync(update, cancellationToken);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return NotFound();
        }

    }
}
