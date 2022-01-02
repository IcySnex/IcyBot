namespace IcyBot.Logic.Helpers;

public class Client
{
    public static async Task<DiscordGuild> Server(ulong ID) =>
        Shared.DiscordClient == null ? throw Exceptions.IsNull("Shared.DiscordClient") : await Shared.DiscordClient.GetGuildAsync(ID);

    public static async Task<DiscordUser> User(ulong ID) =>
        Shared.DiscordClient == null ? throw Exceptions.IsNull("Shared.DiscordClient") : await Shared.DiscordClient.GetUserAsync(ID);

    public static async Task<DiscordMember> Member(ulong ID) =>
        Shared.Server == null ? throw Exceptions.IsNull("Shared.Server") : await Shared.Server.GetMemberAsync(ID);
}