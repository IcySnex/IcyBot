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
        var Bui = Builder.New("Userinfo").WithThumbnail(Logic.Commands.Misc.Avatar(User, Size:256));
        Bui.AddField("Username", $"{User.Username}#{User.Discriminator}", true);
        Bui.AddField("ID", User.Id.ToString(), true);
        Bui.AddField("Flags", User.Flags.ToString(), true);
        Bui.AddField("Nitro", User.PremiumSince is null ? "None" : User.PremiumSince.Value.UtcDateTime.ToString(), true);
        Bui.AddField("Joined Server At", User.JoinedAt.UtcDateTime.ToString(), true);
        Bui.AddField("Joined Discord At", User.CreationTimestamp.UtcDateTime.ToString(), true);
        // FIX ROLES LOL
        Bui.AddField("Roles", string.Join(", ", User.Roles.Select(Role => Role.Mention).ToList()));
        await ctx.RespondAsync(Bui);
    }

    [Command("debug")]
    [Aliases("test")]
    [Description("Command for testing and debugging purposes")]
    public Task Debug(CommandContext ctx, DiscordMember User)
    {
        ctx.RespondAsync(Json.Serialize(Logic.Commands.Misc.Userinfo(User)));
        throw new NotImplementedException();
    }
}