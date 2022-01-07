namespace IcyBot.Logic.Models;

public class BotModel
{
    public BotModel(string Auth = "")
    {
        if (Shared.DiscordClient == null) throw Exceptions.IsNull("Shared.DiscordClient");

        Application = Shared.DiscordClient.CurrentApplication.Name;
        Description = Shared.DiscordClient.CurrentApplication.Description;
        User = $"{Shared.DiscordClient.CurrentUser.Username}#{Shared.DiscordClient.CurrentUser.Discriminator}";
        Icon = Shared.DiscordClient.CurrentApplication.Icon;
        CreatedAt = Shared.DiscordClient.CurrentApplication.CreationTimestamp.UtcDateTime;
        RAM = Local.RamUsage();
        CPU = Local.RunSync(Local.CpuUsage());
        RunningSince = Local.StartTime;
        OS = Local.OsInformation;
        HostIp = Auth == Shared.Config.Auth ? Local.RunSync(Local.IP()) : "auth required";
    }

    public string Application { get; }
    public string Description { get; }
    public string User { get; }
    public string Icon { get; }
    public DateTime CreatedAt { get; }
    public double RAM { get; }
    public double CPU { get; }
    public DateTime RunningSince { get; }
    public string OS { get; }
    public string? HostIp { get; }
}