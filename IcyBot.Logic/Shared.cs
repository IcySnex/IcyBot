using IcyBot.Logic.Helpers.Models;

namespace IcyBot.Logic
{
    public class Shared
    {
        public static ConfigModel Config { get; set; } = ConfigModel.LoadFromFile("Config.json");

        public static List<HelpModel>? Help { get; set; }

        public static DiscordClient? DiscordClient { get; set; }
        public static DiscordConfiguration DiscordConfig { get; } = new()
        {
            Token = Config.Token,
            LogTimestampFormat = "HH:mm:ss | dd.MM.yy",
            MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Error
        };

        public static CommandsNextExtension? CommandsNext { get; set; }
        public static CommandsNextConfiguration CommandsNextConfig { get; } = new()
        {
            StringPrefixes = Config.Prefixes,
            EnableDms = false,
            EnableDefaultHelp = false,
        };
    }
}
