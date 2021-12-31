namespace IcyBot.Logic.Helpers
{
    public class Exceptions
    {
        public static Exception JsonReturnedInvalid = new("Deserialized JSON returned invalid type or null");

        public static Exception IsNull(string ObjectName = "Unknown") => 
            new($"Object '{ObjectName}' is null");
    }
}
