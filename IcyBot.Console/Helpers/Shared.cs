using IcyBot.Console.Helpers.Models;

namespace IcyBot.Console.Helpers
{
    public class Shared
    {
        public static Config Config { get; set; } = Config.LoadFromFile("Config.json");

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
