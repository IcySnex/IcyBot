namespace IcyBot.Logic.Helpers;

public class Discord
{
    public static async Task<DiscordGuild> GetServer(ulong ID) =>
        Shared.DiscordClient == null ? throw Exceptions.IsNull("Shared.DiscordClient") : await Shared.DiscordClient.GetGuildAsync(ID);

    public static async Task<DiscordUser> GetUser(ulong ID) =>
        Shared.DiscordClient == null ? throw Exceptions.IsNull("Shared.DiscordClient") : await Shared.DiscordClient.GetUserAsync(ID);

    public static async Task<DiscordMember> GetMember(ulong ID) =>
        Shared.Server == null ? throw Exceptions.IsNull("Shared.Server") : await Shared.Server.GetMemberAsync(ID);


    public static DiscordEmbedBuilder Builder(string Title = "", string Icon = "", string Description = "", string? Color = null) =>
        new()
        {
            Color = Color == null ? new DiscordColor(Shared.Config.Colors.Main) : new(Color),
            Author = new() { Name = Title, IconUrl = Icon },
            Description = Description,
        };

    public static bool HasRole(DiscordMember User, string RoleName) =>
        User.Roles.Any(Role => RoleName == Role.Name);
    public static bool HasRole(DiscordMember User, params string[] RoleNames) =>
        User.Roles.Any(Role => RoleNames.Contains(Role.Name));
}