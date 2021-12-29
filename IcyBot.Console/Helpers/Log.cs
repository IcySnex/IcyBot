namespace IcyBot.Console.Helpers
{
    public class Log
    {
        public static void Write(string Message, string Type = "", ConsoleColor MessageColor = ConsoleColor.White, ConsoleColor TypeColor = ConsoleColor.DarkCyan)
        {
            System.Console.ForegroundColor = ConsoleColor.DarkGray;
            System.Console.Write($"[{DateTime.Now:HH:mm:ss | dd.MM.yy}] ");
            System.Console.ForegroundColor = TypeColor;
            if (!string.IsNullOrWhiteSpace(Type)) System.Console.Write($"[{Type}] ");
            System.Console.ForegroundColor = MessageColor;
            System.Console.Write(Message + Environment.NewLine);
            System.Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Info(string Message, ConsoleColor Color = ConsoleColor.White) =>
            Write(Message, "Info", Color);
    }
}
