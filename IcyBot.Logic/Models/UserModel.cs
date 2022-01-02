namespace IcyBot.Logic.Models;

public class UserModel
{
    public UserModel(DiscordMember User)
    {
        Username = User.Username;
        Hashtag = int.Parse(User.Discriminator);
        ID = User.Id;
        Flags = User.Flags == null ? Array.Empty<string>() : User.Flags.ToString()!.Split(", ");
        Nitro = User.PremiumSince is null ? null : User.PremiumSince.Value.UtcDateTime;
        JoinedAt = User.JoinedAt.UtcDateTime;
        CreatedAt = User.CreationTimestamp.UtcDateTime;
        Roles = User.Roles.Select(Role => new UserModel_Role(Role.Name, Role.Id)).ToArray();
    }

    public string Username { get; }
    public int Hashtag { get; }
    public ulong ID { get; }
    public string[] Flags { get; }
    public DateTime? Nitro { get; }
    public DateTime JoinedAt { get; }
    public DateTime CreatedAt { get; }
    public UserModel_Role[] Roles { get; }
}

public class UserModel_Role
{
    public UserModel_Role(string Name, ulong ID)
    {
        this.Name = Name;
        this.ID = ID;
    }

    public string Name { get; }
    public ulong ID { get; }
}
