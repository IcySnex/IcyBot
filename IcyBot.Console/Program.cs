using System.Reflection;
using IcyBot.Console.CommandsNext;

namespace IcyBot.Console;

public class Program
{
    public static void Main()
    {
        Log.LogEvent += LogHandler;
        AppDomain.CurrentDomain.UnhandledException += (s, e) => Log.Error((Exception)e.ExceptionObject);
        Start().GetAwaiter().GetResult();
    }

    public static async Task Start()
    {
        Log.Info($"IcyBot, version {Assembly.GetExecutingAssembly().GetName().Version} | DSharpPlus, version {Assembly.LoadFrom("DSharpPlus").GetName().Version}");

        Shared.DiscordClient = new(Shared.DiscordConfig);
        Shared.DiscordClient.Ready += OnReady;
        Log.Info("Initialized DiscordClient");

        Shared.CommandsNext = Shared.DiscordClient.UseCommandsNext(Shared.CommandsNextConfig);
        Shared.CommandsNext.CommandErrored += OnCommandErrored;
        Shared.CommandsNext.CommandExecuted += OnCommandExecuted;
        Shared.CommandsNext.RegisterCommands<Misc>();
        Shared.CommandsNext.RegisterCommands<Moderator>();
        Shared.Help = Logic.Commands.Misc.Help();
        Log.Info("Registered CommandsNext");

        await Shared.DiscordClient.ConnectAsync(new(Shared.Config.Status, ActivityType.Playing));
        Log.Info("Connected DiscordClient to Bot");

        await Task.Delay(-1);
    }

    public static void LogHandler(object? Caller, LogEventArgs e)
    {
        System.Console.ForegroundColor = ConsoleColor.DarkGray;
        System.Console.Write($"[{DateTime.UtcNow:HH:mm:ss | dd.MM.yy} | {Caller}/{e.Method}] ");
        System.Console.ForegroundColor = e.TypeColor;
        if (e.Type != LogType.Nothing) System.Console.Write($"[{e.Type}] ");
        System.Console.ForegroundColor = e.MessageColor;
        System.Console.Write(e.Message + Environment.NewLine);
        System.Console.ForegroundColor = ConsoleColor.White;
    }

    public static async Task OnReady(DiscordClient sender, ReadyEventArgs e)
    {
        //Handlers.Add(sender); //*disabled because no handlers are actually implemented currently
        Shared.Server = await Discord.GetServer(826929957300076544);
        Clock.Start();
        Log.Info("Bot is ready");
    }

    private static Task OnCommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
    {
        if (e.Exception.Message == "Specified command was not found.")
            return Task.CompletedTask;

        DiscordEmbedBuilder Bui = Discord.Builder(Color:Shared.Config.Colors.Error);
        switch (e.Exception.Message)
        {
            case "One or more pre-execution checks failed.":
                Bui.WithAuthor("Damn bro, u aint got no permissions, go fuck urself");
                break;
            case "Could not find a suitable overload for the command.":
                Bui.WithAuthor("Fool doesnt even know how to use this command lmao");
                var Command = Shared.Help!.Find(Category => Category.Name.Equals(e.Command.Module.ModuleType.Name, StringComparison.InvariantCultureIgnoreCase))!.List!.Find(Command => Command.Name.Equals(e.Command.Name, StringComparison.InvariantCultureIgnoreCase))!;
                Bui.WithDescription(Command.List != null && Command.List.Count != 0 ? $"**!{Command}**\n*{Command.Description}*\n\n**Arguments:**\n{string.Join("\n", Command.List.Select(Parameter => $"`{Parameter.Name}`: *{Parameter.Description}*"))}" : $"**!{Command.Name.ToLower()}**\n*{Command.Description}*");
                break;
            default:
                Bui.WithAuthor(e.Exception.Message);
                var Command_ = Shared.Help!.Find(Category => Category.Name.Equals(e.Command.Module.ModuleType.Name, StringComparison.InvariantCultureIgnoreCase))!.List!.Find(Command => Command.Name.Equals(e.Command.Name, StringComparison.InvariantCultureIgnoreCase))!;
                Bui.WithDescription(Command_.List != null && Command_.List.Count != 0 ? $"**!{Command_}**\n*{Command_.Description}*\n\n**Arguments:**\n{string.Join("\n", Command_.List.Select(Parameter => $"`{Parameter.Name}`: *{Parameter.Description}*"))}" : $"**!{Command_.Name.ToLower()}**\n*{Command_.Description}*");
                break;
        }
        e.Context.RespondAsync(Bui);
        Log.Error($"[{e.Context.User.Username}#{e.Context.User.Discriminator}] [{e.Context.Channel.Name}] [Args: {e.Context.RawArgumentString}] Command failed to execute\n{e.Exception.Message}", ConsoleColor.White, "CommandsNext/" + e.Command.Module.ModuleType.Name, e.Command.Name);
        return Task.CompletedTask;
    }
    private static Task OnCommandExecuted(CommandsNextExtension sender, CommandExecutionEventArgs e)
    {
        Log.Success($"[{e.Context.User.Username}#{e.Context.User.Discriminator}] [#{e.Context.Channel.Name}] [Args: {e.Context.RawArgumentString}] Command successfully executed", "CommandsNext/" + e.Command.Module.ModuleType.Name, e.Command.Name);
        return Task.CompletedTask;
    }
}