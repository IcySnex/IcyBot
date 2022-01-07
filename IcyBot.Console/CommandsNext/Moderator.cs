namespace IcyBot.Console.CommandsNext;

[Description("Staff commands for moderating this server")]
public class Moderator : BaseCommandModule
{
    [Command("ban")]
    [Aliases("fuck")]
    [Description("Bans an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]")]
    public async Task Ban(CommandContext ctx,
        [Description("User who should be banned")] DiscordMember User,
        [Description("Reason why this user should be banned")][RemainingText] string Reason = "N/A")
    {
        if (Discord.HasRole(User, "[Owner]", "[Admin]", "[IcyBot]", "[Moderator]", "[Supporter]"))
        {
            await ctx.RespondAsync(Discord.Builder("U fr tried to ban staff? lmao CLOWN", Color: Shared.Config.Colors.Error));
            return;
        }

        ActionModel Action = new(new(User.Username, User.Id), new(ctx.User.Username, ctx.User.Id), Reason, DateTime.UtcNow, null);

        await ctx.RespondAsync(await Logic.Commands.Moderator.Ban(User, Action) ?
            Discord.Builder("Aight I banned this idiot for u, daddy 😘", Description: Action.ToDiscordString()) :
            Discord.Builder("Sowwy daddy, I couldnt ban this idiot 👉👈🥺", Color:Shared.Config.Colors.Error));
    }

    [Command("unban")]
    [Description("Unans an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]")]
    public async Task Unan(CommandContext ctx,
        [Description("User who should be unbanned")] DiscordUser User,
        [Description("Reason why this user should be unbanned")][RemainingText] string Reason = "N/A")
    {
        ActionModel Action = new(new(User.Username, User.Id), new(ctx.User.Username, ctx.User.Id), Reason, DateTime.UtcNow, null);

        await ctx.RespondAsync(await Logic.Commands.Moderator.Unban(User, Action) ?
            Discord.Builder("UNBANNED uwu I hope u are proud of me 👉👈", Description: Action.ToDiscordString(false)) :
            Discord.Builder("U a fucking idiot. this bitch aint even banned", Color:Shared.Config.Colors.Error));
    }
}
