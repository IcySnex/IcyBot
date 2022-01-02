namespace IcyBot.Logic.Helpers;

public class Imaging
{
    public static Color ColorFromHex(string HexCode) => 
        ColorTranslator.FromHtml(HexCode);

    public static async Task<Bitmap> BitmapFromUrl(string Url) =>
        new Bitmap(await Web.Stream(Url));
}