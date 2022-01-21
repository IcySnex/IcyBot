﻿namespace IcyBot.Console.CommandsNext;

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
    [Aliases("unfuck")]
    [Description("Unans an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]")]
    public async Task Unban(CommandContext ctx,
        [Description("User who should be unbanned")] DiscordUser User,
        [Description("Reason why this user should be unbanned")][RemainingText] string Reason = "N/A")
    {
        ActionModel Action = new(new(User.Username, User.Id), new(ctx.User.Username, ctx.User.Id), Reason, DateTime.UtcNow, null);

        await ctx.RespondAsync(await Logic.Commands.Moderator.Unban(User, Action) ?
            Discord.Builder("UNBANNED uwu I hope u are proud of me 👉👈", Description: Action.ToDiscordString(false)) :
            Discord.Builder("U a fucking idiot. this bitch aint even banned", Color:Shared.Config.Colors.Error));
    }

    [Command("tempban")]
    [Aliases("tempfuck")]
    [Description("Temporarily bans an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]")]
    public async Task TempBan(CommandContext ctx,
        [Description("User who should be banned")] DiscordMember User,
        [Description("Amount of time this user should be banned")] TimeSpan Duration,
        [Description("Reason why this user should be banned")][RemainingText] string Reason = "N/A")
    {
        if (Discord.HasRole(User, "[Owner]", "[Admin]", "[IcyBot]", "[Moderator]", "[Supporter]"))
        {
            await ctx.RespondAsync(Discord.Builder("U fr tried to tempban staff? lmao CLOWN", Color: Shared.Config.Colors.Error));
            return;
        }

        ActionModel Action = new(new(User.Username, User.Id), new(ctx.User.Username, ctx.User.Id), Reason, DateTime.UtcNow, DateTime.UtcNow + Duration);

        await ctx.RespondAsync(await Logic.Commands.Moderator.TempBan(User, Action) ?
            Discord.Builder("Aight I tempbanned this idiot for u, daddy 😘", Description: Action.ToDiscordString()) :
            Discord.Builder("Sowwy daddy, I couldnt tempban this idiot 👉👈🥺", Color: Shared.Config.Colors.Error));
    }

    [Command("mute")]
    [Aliases("stfu")]
    [Description("Mutes an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]", "[Supporter]")]
    public async Task Mute(CommandContext ctx,
        [Description("User who should be muted")] DiscordMember User,
        [Description("Reason why this user should be muted")][RemainingText] string Reason = "N/A")
    {
        if (Discord.HasRole(User, "[Owner]", "[Admin]", "[IcyBot]", "[Moderator]", "[Supporter]"))
        {
            await ctx.RespondAsync(Discord.Builder("U kinda dumb ngl, u tried to mute staff", Color: Shared.Config.Colors.Error));
            return;
        }

        ActionModel Action = new(new(User.Username, User.Id), new(ctx.User.Username, ctx.User.Id), Reason, DateTime.UtcNow, null);

        await ctx.RespondAsync(await Logic.Commands.Moderator.Mute(User, Action) ?
            Discord.Builder("Lmao this bi cant speak now", Description: Action.ToDiscordString()) :
            Discord.Builder("Damnnn this aint gonna work..", Color: Shared.Config.Colors.Error));
    }

    [Command("unmute")]
    [Aliases("unstfu")]
    [Description("Unmutes an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]", "[Supporter]")]
    public async Task UnMute(CommandContext ctx,
        [Description("User who should be unmuted")] DiscordMember User,
        [Description("Reason why this user should be unmuted")][RemainingText] string Reason = "N/A")
    {
        ActionModel Action = new(new(User.Username, User.Id), new(ctx.User.Username, ctx.User.Id), Reason, DateTime.UtcNow, null);

        await ctx.RespondAsync(await Logic.Commands.Moderator.Unmute(User, Action) ?
            Discord.Builder("Aight this bi can speak now", Description: Action.ToDiscordString(false)) :
            Discord.Builder("Damn bro mf prob aint even muted lmao", Color: Shared.Config.Colors.Error));
    }

    [Command("tempmute")]
    [Aliases("tempstfu")]
    [Description("Temporarily mutes an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]", "[Supporter]")]
    public async Task TempMute(CommandContext ctx,
        [Description("User who should be muted")] DiscordMember User,
        [Description("Amount of time this user should be muted")] TimeSpan Duration,
        [Description("Reason why this user should be muted")][RemainingText] string Reason = "N/A")
    {
        if (Discord.HasRole(User, "[Owner]", "[Admin]", "[IcyBot]", "[Moderator]", "[Supporter]"))
        {
            await ctx.RespondAsync(Discord.Builder("U kinda dumb ngl, u tried to mute staff", Color: Shared.Config.Colors.Error));
            return;
        }

        ActionModel Action = new(new(User.Username, User.Id), new(ctx.User.Username, ctx.User.Id), Reason, DateTime.UtcNow, DateTime.UtcNow + Duration);

        await ctx.RespondAsync(await Logic.Commands.Moderator.TempMute(User, Action) ?
            Discord.Builder("Lmao this bi cant speak now", Description: Action.ToDiscordString()) :
            Discord.Builder("Damnnn this aint gonna work..", Color: Shared.Config.Colors.Error));
    }

    [Command("invisible")]
    [Aliases("hide")]
    [Description("Invisibles an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]", "[Supporter]")]
    public async Task Invisible(CommandContext ctx,
        [Description("User who should be invisibled")] DiscordMember User,
        [Description("Reason why this user should be invisibled")][RemainingText] string Reason = "N/A")
    {
        if (Discord.HasRole(User, "[Owner]", "[Admin]", "[IcyBot]", "[Moderator]", "[Supporter]"))
        {
            await ctx.RespondAsync(Discord.Builder("U lookin stoopid, u cant invisible staff", Color: Shared.Config.Colors.Error));
            return;
        }

        ActionModel Action = new(new(User.Username, User.Id), new(ctx.User.Username, ctx.User.Id), Reason, DateTime.UtcNow, null);

        await ctx.RespondAsync(await Logic.Commands.Moderator.Invisible(User, Action) ?
            Discord.Builder("Lmao this mf cant see shit now", Description: Action.ToDiscordString()) :
            Discord.Builder("Ayo something bad happened..", Color: Shared.Config.Colors.Error));
    }

    [Command("uninvisible")]
    [Aliases("unhide")]
    [Description("Uninvisibles an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]", "[Supporter]")]
    public async Task Uninvisible(CommandContext ctx,
        [Description("User who should be uninvisibled")] DiscordMember User,
        [Description("Reason why this user should be uninvisibled")][RemainingText] string Reason = "N/A")
    {
        ActionModel Action = new(new(User.Username, User.Id), new(ctx.User.Username, ctx.User.Id), Reason, DateTime.UtcNow, null);

        await ctx.RespondAsync(await Logic.Commands.Moderator.Uninvisible(User, Action) ?
            Discord.Builder("Mf can see shit now again", Description: Action.ToDiscordString(false)) :
            Discord.Builder("Damn bro bitch prob aint even invisibled", Color: Shared.Config.Colors.Error));
    }

    [Command("tempinvisible")]
    [Aliases("temphide")]
    [Description("Temporarily invisibles an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]", "[Supporter]")]
    public async Task TempInvisible(CommandContext ctx,
        [Description("User who should be invisibled")] DiscordMember User,
        [Description("Amount of time this user should be invisibled")] TimeSpan Duration,
        [Description("Reason why this user should be invisibled")][RemainingText] string Reason = "N/A")
    {
        if (Discord.HasRole(User, "[Owner]", "[Admin]", "[IcyBot]", "[Moderator]", "[Supporter]"))
        {
            await ctx.RespondAsync(Discord.Builder("U lookin stoopid, u cant invisible staff", Color: Shared.Config.Colors.Error));
            return;
        }

        ActionModel Action = new(new(User.Username, User.Id), new(ctx.User.Username, ctx.User.Id), Reason, DateTime.UtcNow, DateTime.UtcNow + Duration);

        await ctx.RespondAsync(await Logic.Commands.Moderator.TempInvisible(User, Action) ?
            Discord.Builder("Lmao this mf cant see shit now", Description: Action.ToDiscordString()) :
            Discord.Builder("Ayo something bad happened..", Color: Shared.Config.Colors.Error));
    }

    [Command("kick")]
    [Aliases("remove")]
    [Description("Kicks an user from this server")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]", "[Supporter]")]
    public async Task Kick(CommandContext ctx,
        [Description("User who should be kicked")] DiscordMember User,
        [Description("Reason why this user should be kicked")][RemainingText] string Reason = "N/A")
    {
        if (Discord.HasRole(User, "[Owner]", "[Admin]", "[IcyBot]", "[Moderator]", "[Supporter]"))
        {
            await ctx.RespondAsync(Discord.Builder("why tf would you kick staff lol", Color: Shared.Config.Colors.Error));
            return;
        }

        await ctx.RespondAsync(await Logic.Commands.Moderator.Kick(User, ctx.User.Username, Reason) ?
            Discord.Builder("mfs gone now lol", Description: $"**Reason:** {Reason}\n**By:** {ctx.User.Mention}, {Text.FormatDate(DateTime.UtcNow)}") :
            Discord.Builder("ERROR ERROR ERROR ERROR", Color: Shared.Config.Colors.Error));
    }

    [Command("clear")]
    [Aliases("purge")]
    [Description("Clears messages in the current channel")]
    [RequireRoles(RoleCheckMode.Any, "[Owner]", "[Admin]", "[Moderator]", "[Supporter]")]
    public async Task Clear(CommandContext ctx, [Description("Amount of messages to delete")][RemainingText] int Amount)
    {
        var Message = await ctx.RespondAsync(await Logic.Commands.Moderator.Clear(ctx.Channel, Amount) ?
            Discord.Builder($"AYO I cleared {Amount} messages!!!!!") :
            Discord.Builder("Damn this not gonna work", Color: Shared.Config.Colors.Error));
        await Task.Delay(3000);
        try { await Message.DeleteAsync(); } catch { }
    }
}
