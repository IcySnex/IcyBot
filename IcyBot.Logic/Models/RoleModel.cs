namespace IcyBot.Logic.Models;

public class RoleModel
{
    public RoleModel(DiscordRole Role)
    {
        Name = Role.Name;
        ID = Role.Id;
        Color = Role.Color.ToString();
        IsMentionable = Role.IsMentionable;
        Position = Role.Position;
        CreatedAt = Role.CreationTimestamp.UtcDateTime;
        Permissions = Role.Permissions.ToPermissionString().Split(", ");
    }

    public string Name { get; }
    public ulong ID { get; }
    public string Color { get; }
    public bool IsMentionable { get; }
    public int Position { get; }
    public DateTime CreatedAt { get; }
    public string[] Permissions { get; }
}