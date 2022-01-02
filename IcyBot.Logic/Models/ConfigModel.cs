namespace IcyBot.Logic.Helpers.Models;

public class ConfigModel
{
    public static ConfigModel LoadFromFile(string Path) =>
        Json.Deserialize<ConfigModel>(Path);

    public void Save(string Path) =>
        Json.SerializeToFile(this, Path);

    public string Token { get; set; } = "?";
    public string? Status { get; set; } = "ur mom";
    public string Auth { get; set; } = "Password";
    public string[] Prefixes { get; } = { "!", "?", "." };
    public ConfigModel_Colors Colors { get; set; } = new();
    public ConfigModel_Roles Roles { get; set; } = new();
}

public class ConfigModel_Colors
{
    public string Main { get; set; } = "DD99FF";
    public string Error { get; set; } = "FF676A";
}

public class ConfigModel_Roles
{
    public ulong Muted { get; set; } = 826965022706106381;
    public ulong Invisible { get; set; } = 826965192667430933;
}