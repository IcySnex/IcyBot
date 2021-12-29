global using System.Drawing;
global using DSharpPlus;
global using DSharpPlus.Entities;
global using DSharpPlus.EventArgs;
global using DSharpPlus.CommandsNext;
global using DSharpPlus.CommandsNext.Attributes;
global using IcyBot.Console;
global using IcyBot.Console.Helpers;
using System.Reflection;
using IcyBot.Console.CommandsNext;

StartBot().GetAwaiter().GetResult();

async Task StartBot()
{
    Log.Info($"[Program] IcyBot, version {Assembly.GetExecutingAssembly().GetName().Version} | DSharpPlus, version {Assembly.LoadFrom("DSharpPlus").GetName().Version}");

    Shared.DiscordClient = new(Shared.DiscordConfig);
    Shared.DiscordClient.Ready += Onready;
    Log.Info("[Program] Initialized DiscordClient");

    Shared.CommandsNext = Shared.DiscordClient.UseCommandsNext(Shared.CommandsNextConfig);
    Shared.CommandsNext.RegisterCommands<Misc>();

    await Shared.DiscordClient.ConnectAsync(new(Shared.Config.Status, ActivityType.Playing));
    Log.Info("[Program] Connected DiscordClient to Bot");

    await Task.Delay(-1);
}

Task Onready(DiscordClient sender, ReadyEventArgs e)
{
    //Handlers.Add(sender); *disabled because no handlers are actually implemented currently
    Log.Info("[Program] Bot is ready");
    return Task.CompletedTask;
}