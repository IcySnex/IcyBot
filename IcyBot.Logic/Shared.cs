namespace IcyBot.Logic;

public class Shared
{
    public static ConfigModel Config { get; } = Json.Deserialize<ConfigModel>("Config");

    public static List<ActionModel> Bans { get; } = Json.Load<List<ActionModel>>("Actions/Bans", new());
    public static List<ActionModel> TempBans { get; } = Json.Load<List<ActionModel>>("Actions/TempBans", new());

    public static List<HelpModel>? Help { get; set; }
    public static DiscordGuild? Server { get; set; }

    public static string Invite { get; } = "https://discord.com/invite/Z8kweuryux";
    public static string Project { get; } = "https://github.com/IcySnex/IcyBot";

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