namespace IcyBot.Logic.Models
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string Message, ConsoleColor MessageColor = ConsoleColor.White, LogType Type = LogType.Nothing, ConsoleColor TypeColor = ConsoleColor.DarkCyan, string? Method = null)
        {
            this.Message = Message;
            this.MessageColor = MessageColor;
            this.Type = Type;
            this.TypeColor = TypeColor;
            this.Method = Method;
        }

        public string Message { get; }
        public ConsoleColor MessageColor { get; }
        public LogType Type { get; }
        public ConsoleColor TypeColor { get; }
        public string? Method { get; }
    }
}
