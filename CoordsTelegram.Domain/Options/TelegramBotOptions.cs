using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordsTelegram.Domain.Options
{
    public class TelegramBotOptions
    {
        public static readonly string SectionName = "BotOptions";

        public string BotToken { get; init; } = default!;
        public string HostAddress { get; init; } = default!;
        public string Route { get; init; } = default!;
        public string BotName { get; init; } = default!;
    }
}
