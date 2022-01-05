﻿using System.Drawing;

namespace IcyBot.Logic.Commands;

public class Misc
{
    public static List<HelpModel> Help()
    {
        if (Shared.CommandsNext == null) throw Exceptions.IsNull("Shared.CommandsNext");

        List<HelpModel> Result = new();
        foreach (var Command in Shared.CommandsNext.RegisteredCommands.Values)
        {
            if (Result.Any(ResultCategory => ResultCategory.Name == Command.Module.ModuleType.Name))
            {
                var Category = Result.First(ResultCategory => ResultCategory.Name == Command.Module.ModuleType.Name);
                if (!Category.List!.Any(CategoryCommand => CategoryCommand.Description == Command.Description))
                    Category.List!.Add(new(HelpModelType.Command, Text.Fltu(Command.Name), Command.Description, Command.Overloads[0].Arguments.Select(CommandArgument => CommandArgument.IsOptional ? new HelpModel(HelpModelType.Parameter, $" (optional {Text.Fltu(CommandArgument.Name)})", CommandArgument.Description) : new HelpModel(HelpModelType.Parameter, $" [{Text.Fltu(CommandArgument.Name)}]", CommandArgument.Description)).ToList()));
            }
            else
            {
                object[] Attributes = Command.Module.ModuleType.GetCustomAttributes(typeof(DescriptionAttribute), false);
                Result.Add(new(HelpModelType.Category, Text.Fltu(Command.Module.ModuleType.Name), Attributes.Length == 0 ? "N/A" : ((DescriptionAttribute)Attributes[0]).Description));
            }
        }
        return Result;
    }

    public static string Avatar(DiscordUser User, Enums.ImageFormat Format = Enums.ImageFormat.Auto, ushort Size = 1024) => 
       User.GetAvatarUrl((DSharpPlus.ImageFormat)Format, Size);

    public static int Latency() =>
        Shared.DiscordClient == null ? throw Exceptions.IsNull("Shared.DiscordClient") : Shared.DiscordClient.Ping;

    public static UserModel Userinfo(DiscordMember User) =>
        new(User);

    public static RoleModel Roleinfo(DiscordRole Role) =>
        new(Role);

    public static ServerModel Serverinfo(DiscordGuild Server) =>
        new(Server);

    public static BotModel Botinfo(string Auth = "") =>
        new(Auth);
}