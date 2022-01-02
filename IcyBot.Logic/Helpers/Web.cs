namespace IcyBot.Logic.Helpers;

public class Web
{
    private static HttpClient Client = new();

    public static async Task<Stream> Stream(string url) =>
        await Client.GetStreamAsync(url);

}