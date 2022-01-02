namespace IcyBot.Logic.Helpers;

public class Text
{
    public static string Fltu(string Input) =>
        string.Concat(Input[0].ToString().ToUpper(), Input.AsSpan(1));
}