namespace IcyBot.Logic.Models;

public class UserModel
{
    public UserModel(DiscordMember User)
    {
        Username = User.Username;
        Hashtag = User.Discriminator;
        ID = User.Id;
        //Warns = Commands.Misc.Warns(User);
        Flags = User.Flags == null ? Array.Empty<string>() : User.Flags.ToString()!.Split(", ");
        JoinedAt = User.JoinedAt.UtcDateTime;
        CreatedAt = User.CreationTimestamp.UtcDateTime;
        Roles = User.Roles.Select(Role => new UserModel_Role(Role.Name, Role.Id)).ToArray();
    }

    public string Username { get; }
    public string Hashtag { get; }
    public ulong ID { get; }
    public int Warns { get; }
    public string[] Flags { get; }
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
