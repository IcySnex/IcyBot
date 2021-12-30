﻿using System.Reflection;
using System.Text;
using IcyBot.Console.CommandsNext;

namespace IcyBot.Console
{
    public class Program
    {
        public static void Main()
        {
            Log.LogEvent += LogHandler;
            AppDomain.CurrentDomain.UnhandledException += (s, e) => Log.Error((Exception)e.ExceptionObject);
            StartBot().GetAwaiter().GetResult();
        }

        public static async Task StartBot()
        {
            Log.Info($"IcyBot, version {Assembly.GetExecutingAssembly().GetName().Version} | DSharpPlus, version {Assembly.LoadFrom("DSharpPlus").GetName().Version}");

            Shared.DiscordClient = new(Shared.DiscordConfig);
            Shared.DiscordClient.Ready += OnReady;
            Log.Info("Initialized DiscordClient");

            Shared.CommandsNext = Shared.DiscordClient.UseCommandsNext(Shared.CommandsNextConfig);
            Shared.CommandsNext.CommandErrored += OnCommandErrored;
            Shared.CommandsNext.CommandExecuted += OnCommandExecuted;
            Shared.CommandsNext.RegisterCommands<Misc>();
            Log.Info("Registered CommandsNext");

            await Shared.DiscordClient.ConnectAsync(new(Shared.Config.Status, ActivityType.Playing));
            Log.Info("Connected DiscordClient to Bot");

            await Task.Delay(-1);
        }

        public static void LogHandler(object? Caller, LogEventArgs e)
        {
            System.Console.ForegroundColor = ConsoleColor.DarkGray;
            System.Console.Write($"[{DateTime.Now:HH:mm:ss | dd.MM.yy} | {Caller}/{e.Method}] ");
            System.Console.ForegroundColor = e.TypeColor;
            if (e.Type != LogType.Nothing) System.Console.Write($"[{e.Type}] ");
            System.Console.ForegroundColor = e.MessageColor;
            System.Console.Write(e.Message + Environment.NewLine);
            System.Console.ForegroundColor = ConsoleColor.White;
        }

        public static Task OnReady(DiscordClient sender, ReadyEventArgs e)
        {
            //Handlers.Add(sender); *disabled because no handlers are actually implemented currently
            Log.Info("Bot is ready");
            return Task.CompletedTask;
        }

        private static Task OnCommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            if (e.Exception.Message != "Specified command was not found.")
            {
                Log.Error($"[{e.Context.User.Username}#{e.Context.User.Discriminator}] [{e.Context.Channel.Name}] [Args: {e.Context.RawArgumentString}] Command failed to execute\n{e.Exception.Message}", "CommandsNext/" + e.Command.Module.ModuleType.Name, e.Command.Name);
            }
            return Task.CompletedTask;
        }
        private static Task OnCommandExecuted(CommandsNextExtension sender, CommandExecutionEventArgs e)
        {
            Log.Success($"[{e.Context.User.Username}#{e.Context.User.Discriminator}] [#{e.Context.Channel.Name}] [Args: {e.Context.RawArgumentString}] Command successfully executed", "CommandsNext/" + e.Command.Module.ModuleType.Name, e.Command.Name);
            return Task.CompletedTask;
        }
    }
}