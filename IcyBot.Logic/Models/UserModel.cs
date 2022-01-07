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
        Roles = User.Roles.Select(Role => new EntityModel(Role.Name, Role.Id)).ToArray();
    }

    public string Username { get; }
    public string Hashtag { get; }
    public ulong ID { get; }
    public int Warns { get; }
    public string[] Flags { get; }
    public DateTime JoinedAt { get; }
    public DateTime CreatedAt { get; }
    public EntityModel[] Roles { get; }
}
