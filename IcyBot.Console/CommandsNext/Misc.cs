namespace IcyBot.Console.CommandsNext;

[Description("General commands for every purpose which usable by any user")]
public class Misc : BaseCommandModule
{
    [Command("help")]
    [Aliases("commands")]
    [Description("Sends a help message with all commands")]
    public async Task Help(CommandContext ctx, [Description("Get help for a specific category")][RemainingText] string Category = "None")
    {
        DiscordEmbedBuilder Bui = Builder.New();
        var TryRequestedCategory = Shared.Help!.Where(Category_ => Category_.Name.Equals(Category, StringComparison.InvariantCultureIgnoreCase));
        if (TryRequestedCategory.Count() == 0)
        {
            foreach (var Category_ in Shared.Help!)
                Bui.AddField(Category_.Name, $"!help {Category_.Name.ToLower()}", true);
            Bui.WithAuthor($"{ctx.Guild.Name} - Commands");
        }
        else
        {
            string Description = "";
            var RequestedCategory = TryRequestedCategory.First();
            foreach (var Command in RequestedCategory.List!)
                Description += $"`!{Command}`\n*{Command.Description}*\n\n";
            Bui.WithAuthor($"{RequestedCategory.Name} - Commands");
            Bui.WithDescription(Description);
        }
        await ctx.RespondAsync(Bui);
    }

    [Command("avatar")]
    [Aliases("av")]
    [Description("Displays the user avater from a member")]
    public async Task Avatar(CommandContext ctx,
        [Description("User from whom the avatar should be displayed")] DiscordUser? User = null,
        [Description("The size with wich the avatar should be displayed (16, 32, 64, 128, etc)")] ushort Size = 1024)
    {
        if (User is null) User = ctx.User;
        var Bui = Builder.New($"Avatar - {User.Username}");
        Bui.WithImageUrl(Logic.Commands.Misc.Avatar(User, Size: Size));
        await ctx.RespondAsync(Bui);
    }

    [Command("ping")]
    [Aliases("latency")]
    [Description("Displays the latency between the bot and Discord")]
    public async Task Avatar(CommandContext ctx) =>
        await ctx.RespondAsync(Builder.New("Server Latency", Description: $"**Pong!** ({Logic.Commands.Misc.Latency()}ms)"));

    [Command("invite")]
    [Aliases("inv")]
    [Description("Displays a permanent invite to this server")]
    public async Task Invite(CommandContext ctx) =>
        await ctx.RespondAsync(Builder.New(Description: $"`{Shared.Invite}`").WithAuthor($"Server Invite - {ctx.Guild.Name}", Shared.Invite).WithFooter("(You can also press on the title!)"));

    [Command("userinfo")]
    [Aliases("user-info", "user", "ui")]
    [Description("Displays all important informations about a user")]
    public async Task Userinfo(CommandContext ctx, [Description("User from whom the informations be displayed")] DiscordMember? User = null)
    {
        if (User is null) User = ctx.Member;
        var Bui = Builder.New("Userinfo").WithThumbnail(Logic.Commands.Misc.Avatar(User, Size:128));
        Bui.AddField("Username", $"{User.Username}#{User.Discriminator}", true);
        Bui.AddField("ID", User.Id.ToString(), true);
        //Bui.AddField("Warns", Logic.Commands.Misc.Warns(User), true);
        Bui.AddField("Flags", User.Flags == null ? "None" : User.Flags.ToString(), true);
        Bui.AddField("Joined Server At", User.JoinedAt.UtcDateTime.ToString(), true);
        Bui.AddField("Joined Discord At", User.CreationTimestamp.UtcDateTime.ToString(), true);
        Bui.AddField("Roles", string.Join(", ", User.Roles.Select(Role => Role.Mention).ToList()));
        await ctx.RespondAsync(Bui);
    }

    [Command("roleinfo")]
    [Aliases("role-info", "role", "rl")]
    [Description("Displays all important informations about a role")]
    public async Task Roleinfo(CommandContext ctx, [Description("Role from whom the informations be displayed")] DiscordRole Role)
    {
        var Bui = Builder.New("Roleinfo", Color:Role.Color);
        Bui.AddField("Role", Role.Mention, true);
        Bui.AddField("ID", Role.Id.ToString(), true);
        Bui.AddField("Color", Role.Color.ToString(), true);
        Bui.AddField("Is Mentionable", Role.IsMentionable.ToString(), true);
        Bui.AddField("Position", $"{Role.Position}/{ctx.Guild.Roles.Count - 1}", true);
        Bui.AddField("Created At", Role.CreationTimestamp.UtcDateTime.ToString(), true);
        Bui.AddField("Permissions", Role.Permissions.ToPermissionString());
        await ctx.RespondAsync(Bui);
    }
    
    [Command("serverinfo")]
    [Aliases("server-info", "server", "si")]
    [Description("Displays all important informations about a role")]
    public async Task Serverinfo(CommandContext ctx)
    {
        var Bui = Builder.New().WithAuthor("Serverinfo", Shared.Invite).WithThumbnail(ctx.Guild.IconUrl);
        Bui.AddField("Name", ctx.Guild.Name, true);
        Bui.AddField("ID", ctx.Guild.Id.ToString(), true);
        Bui.AddField("Owner", ctx.Guild.Owner.Mention, true);
        Bui.AddField("Boosts", ctx.Guild.PremiumSubscriptionCount.ToString(), true);
        Bui.AddField("Stats", $"Total Members: **{ctx.Guild.MemberCount}**\nTotal Channels: **{ctx.Guild.Channels.Count}**\nTotal Roles: **{ctx.Guild.Roles.Count - 1}**\nTotal Emojis: **{ctx.Guild.Emojis.Count}**", true);
        Bui.AddField("Created At", ctx.Guild.CreationTimestamp.UtcDateTime.ToString(), true);
        Bui.AddField("Features", Text.Fltu(string.Join(", ", ctx.Guild.Features).ToLower(), true).Replace("_", " "));
        await ctx.RespondAsync(Bui);
    }
    
    [Command("botinfo")]
    [Aliases("bot-info", "bot", "bi")]
    [Description("Displays all important informations about this bot")]
    public async Task Botinfo(CommandContext ctx, [Description("Password required to view host IP-address")] [RemainingText] string Auth = "")
    {
        var Bui = Builder.New().WithAuthor("Botinfo", Shared.Project).WithThumbnail(Shared.DiscordClient!.CurrentApplication.Icon);
        Bui.AddField("Name", $"{Shared.DiscordClient.CurrentApplication.Name} / {Shared.DiscordClient.CurrentUser.Username}#{Shared.DiscordClient!.CurrentUser.Discriminator}", true);
        Bui.AddField("Description", Shared.DiscordClient.CurrentApplication.Description, true);
        Bui.AddField("Created At", Shared.DiscordClient.CurrentApplication.CreationTimestamp.UtcDateTime.ToString(), true);
        Bui.AddField("RAM Usage", $"{Local.RamUsage().ToString("F")} MB", true);
        Bui.AddField("CPU Usage", $"{(await Local.CpuUsage()).ToString("F")} %", true);
        Bui.AddField("Running for", Text.FormatTime(Local.RunningFor()), true);
        Bui.AddField("Latency", $"{Logic.Commands.Misc.Latency()}ms", true);
        Bui.AddField("OS Information", Local.OsInformation, true);
        Bui.AddField("Host IP", Auth == Shared.Config.Auth ? await Local.IP() : "*`auth required`*", true);
        await ctx.RespondAsync(Bui);
    }

    [Command("debug")]
    [Aliases("test")]
    [Description("Command for testing and debugging purposes")]
    public Task Debug(CommandContext ctx, string auth = "")
    { 
        throw new NotImplementedException("This is normal lol");
    }
}