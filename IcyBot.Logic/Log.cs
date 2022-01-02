using System.Runtime.CompilerServices;

namespace IcyBot.Logic;

public class Log
{
    public static event EventHandler<LogEventArgs>? LogEvent;

    private static void Invoke(string Caller, LogEventArgs e) =>
        LogEvent!.Invoke(Caller.Split('\\').Last().Replace(".cs", ""), e);

    public static void Write(string Message, ConsoleColor Color = ConsoleColor.White, [CallerFilePath] string Caller = "", [CallerMemberName] string Method = "") =>
        Invoke(Caller, new(Message, Color, LogType.Nothing, ConsoleColor.White, Method));

    public static void Info(string Message, ConsoleColor Color = ConsoleColor.White, [CallerFilePath] string Caller = "", [CallerMemberName] string Method = "") =>
        Invoke(Caller, new(Message, Color, LogType.Information, ConsoleColor.DarkCyan, Method));

    public static void Success(string Message, [CallerFilePath] string Caller = "", [CallerMemberName] string Method = "") =>
        Invoke(Caller, new(Message, ConsoleColor.White, LogType.Success, ConsoleColor.Green, Method));

    public static void Error(string Message, [CallerFilePath] string Caller = "", [CallerMemberName] string Method = "") =>
        Invoke(Caller, new(Message, ConsoleColor.White, LogType.Error, ConsoleColor.Red, Method));
    public static void Error(Exception Exception, [CallerFilePath] string Caller = "", [CallerMemberName] string Method = "") =>
        Invoke(Caller, new(Exception.InnerException is null ? Exception.Message : $"{Exception.Message}\n({Exception.InnerException.Message})", ConsoleColor.White, LogType.Error, ConsoleColor.Red, Method));

    public static void Warning(string Message, [CallerFilePath] string Caller = "", [CallerMemberName] string Method = "") =>
        Invoke(Caller, new(Message, ConsoleColor.White, LogType.Warning, ConsoleColor.Yellow, Method));
}