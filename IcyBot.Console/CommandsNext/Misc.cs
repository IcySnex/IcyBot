namespace IcyBot.Console.CommandsNext
{
    public class Misc : BaseCommandModule
    {
        [Command("hi")]
        public async Task hi(CommandContext ctx)
        {
            await ctx.RespondAsync("fock yo!");
        }
    }
}
