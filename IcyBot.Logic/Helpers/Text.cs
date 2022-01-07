using System.Globalization;

namespace IcyBot.Logic.Helpers;

public class Text
{
    public static string Fltu(string Input, bool PerWord = false) =>
        PerWord ? new CultureInfo("en-US", false).TextInfo.ToTitleCase(Input) : string.Concat(Input[0].ToString().ToUpper(), Input.AsSpan(1));

    public static string FormatTime(TimeSpan Time, string Format = "{dd} days, {hh} hours, {mm} minutes, {ss} seconds") => Format
        .Replace("{dd}", Time.Days.ToString())
        .Replace("{hh}", Time.Hours.ToString())
        .Replace("{mm}", Time.Minutes.ToString())
        .Replace("{ss}", Time.Seconds.ToString())
        .Replace("{ms}", Time.Milliseconds.ToString());

    public static string FormatDate(DateTime? Date = null, string Attribute = "f") =>
        $"<t:{(long)((Date is null ? DateTime.UtcNow : Date.Value) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds}:{Attribute}>";
}