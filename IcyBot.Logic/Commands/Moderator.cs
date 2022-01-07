namespace IcyBot.Logic.Commands;

public class Moderator
{
    public static async Task<bool> Ban(DiscordMember User, ActionModel Action)
    {
        try
        { 
            await User.SendMessageAsync(Discord.Builder($"Damn bro u got banned from 'IcyCord' lol.", Description:Action.ToString()));
        }
        catch { Log.Warning("Could not send direct message user who got banned!"); }

        try
        { 
            await User.BanAsync(reason: Action.Reason);
            Log.Info($"User Banned - {Action}", ConsoleColor.Yellow, "Logs");
            Shared.Bans.Add(Action);
            Json.SerializeToFile(Shared.Bans, "Actions/Bans");
            return true; 
        }
        catch { return false; }
    }

    public static async Task<bool> Unban(DiscordUser User, ActionModel Action)
    {
        if (Shared.Server is null) throw Exceptions.IsNull("Shared.Server");

        try
        {
            await User.UnbanAsync(Shared.Server, reason: Action.Reason);
            Log.Info($"User Unanned - {Action}", ConsoleColor.Yellow, "Logs");
            if (Shared.Bans.Find(Ban => Action.User.ID == Ban.User.ID) is ActionModel Ban)
            {
                Shared.Bans.Remove(Ban);
                Json.SerializeToFile(Shared.Bans, "Actions/Bans");
            }
            return true; 
        }
        catch { return false; }
    }
    public static async Task<bool> Unban(ulong User)
    {
        if (Shared.Server is null) throw Exceptions.IsNull("Shared.Server");

        try
        {
            await Shared.Server.UnbanMemberAsync(User);
            Log.Info($"User Unanned - User: {User}", ConsoleColor.Yellow, "Logs");
            return true; 
        }
        catch { return false; }
    }
}