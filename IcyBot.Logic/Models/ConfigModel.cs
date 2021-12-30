namespace IcyBot.Logic.Helpers.Models
{
    public class ConfigModel
    {
        public static ConfigModel LoadFromFile(string Path) =>
            Json.Deserialize<ConfigModel>(Path);

        public string Token { get; set; } = "?";
        public string? Status { get; set; } = "ur mom";
        public string Auth { get; set; } = "Password";
        public string[] Prefixes { get; } = { "!", "?", "." };
        public Config_Colors Colors { get; set; } = new();
        public Config_Roles Roles { get; set; } = new();
    }

    public class Config_Colors
    {
        public Color Main { get; set; } = Imaging.ColorFromHex("DD99FF");
        public Color Error { get; set; } = Imaging.ColorFromHex("FF676A");
    }

    public class Config_Roles
    {
        public ulong Muted { get; set; } = 826965022706106381;
        public ulong Invisible { get; set; } = 826965192667430933;
    }
}
