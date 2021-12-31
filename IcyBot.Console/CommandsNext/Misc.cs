namespace IcyBot.Console.CommandsNext
{
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
            } else
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
        [Description("Shows the user avater from a member")]
        public async Task Avatar(CommandContext ctx, [Description("User from whom the avatar should be shown")] DiscordMember User)
        {
            await ctx.RespondAsync("fock yo!");
        }

        [Command("debug")]
        [Aliases("test")]
        [Description("Command for testing and debugging purposes")]
        public async Task Debug(CommandContext ctx)
        {
            await ctx.RespondAsync("null");
        }
    }
}
