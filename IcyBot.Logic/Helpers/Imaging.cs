namespace IcyBot.Logic.Helpers
{
    public class Imaging
    {
        public static Color ColorFromHex(string HexCode) => ColorTranslator.FromHtml($"#{HexCode}");
    }
}
