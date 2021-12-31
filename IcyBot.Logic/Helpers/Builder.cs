namespace IcyBot.Logic.Helpers
{
    public class Builder
    {
        public static DiscordEmbedBuilder New(string Title = "", string Icon = "", string Description = "", DiscordColor? Color = null) =>
            new()
            {
                Color = Color == null ? new(Shared.Config.Colors.Main) : Color.Value,
                Author = new() { Name = Title, IconUrl = Icon },
                Description = Description,
            };
    }
}
