namespace IcyBot.Logic.Models;

public class ServerModel
{
    public ServerModel(DiscordGuild Server)
    {
        Name = Server.Name;
        ID = Server.Id;
        Icon = Server.IconUrl;
        Owner = Server.OwnerId;
        Boosts = Server.PremiumSubscriptionCount.HasValue ? Server.PremiumSubscriptionCount.Value : 0;
        Stats = new(Server.MemberCount, Server.Channels.Count, Server.Roles.Count - 1, Server.Emojis.Count);
        CreatedAt = Server.CreationTimestamp.UtcDateTime;
        Features = Text.Fltu(string.Join(";", Server.Features).ToLower(), true).Replace("_", " ").Split(";");
    }

    public string Name { get; }
    public ulong ID { get; }
    public string Icon { get; }
    public ulong Owner { get; }
    public int Boosts { get; }
    public ServerModel_Stats Stats { get; }
    public DateTime CreatedAt { get; }
    public string[] Features { get; }
}

public class ServerModel_Stats
{
    public ServerModel_Stats(int Members, int Channels, int Roles, int Emojis)
    {
        this.Members = Members;
        this.Channels = Channels;
        this.Roles = Roles;
        this.Emojis = Emojis;
    }

    public int Members { get; }
    public int Channels { get; }
    public int Roles { get; }
    public int Emojis { get; }
}