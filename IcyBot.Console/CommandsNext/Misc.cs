namespace IcyBot.Console.CommandsNext
{
    public class Misc : BaseCommandModule
    {
        [Command("hi")]
        public async Task hi(CommandContext ctx)
        {
            int zero = 0;
            await ctx.RespondAsync("fock yo!");
        }
    }
}
