namespace IcyBot.Logic.Models;

public class ConfigModel
{
    public void Save(string Path) =>
        Json.SerializeToFile(this, Path);

    public string Token { get; set; } = "?";
    public string? Status { get; set; } = "on this server...";
    public string Auth { get; set; } = "Password";
    public string[] Prefixes { get; } = { "!" };
    public ConfigModel_Colors Colors { get; set; } = new();
    public ConfigModel_Roles Roles { get; set; } = new();
    public int ClockInterval { get; set; } = 60000;
}

public class ConfigModel_Colors
{
    public string Main { get; set; } = "4287f5";
    public string Error { get; set; } = "f54242";
}

public class ConfigModel_Roles
{
    public ulong Muted { get; set; } = 000000000000000000;
    public ulong Invisible { get; set; } = 000000000000000000;
}