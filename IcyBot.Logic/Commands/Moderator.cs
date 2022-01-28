namespace IcyBot.Logic.Commands;

public class Moderator
{
    public static async Task<bool> Ban(DiscordMember User, ActionModel Action)
    {
        try
        { 
            await User.SendMessageAsync(Discord.Builder($"Damn bro u got banned from 'IcyCord' lol.", Description:Action.ToDiscordString()));
        }
        catch { Log.Warning("Could not send direct message user who got banned!"); }

        try
        { 
            await User.BanAsync(reason: Action.Reason);
            Shared.Bans.Add(Action);
            Json.SerializeToFile(Shared.Bans, "Actions/Bans");
            Log.Info($"User banned - {Action}", ConsoleColor.Yellow, "Logs");
            return true; 
        }
        catch { return false; }
    }

    public static async Task<bool> Unban(ActionModel Action)
    {
        if (Shared.Server is null) throw Exceptions.IsNull("Shared.Server");

        try
        {
            await Shared.Server.UnbanMemberAsync(Action.User.ID, Action.Reason);
            if (Shared.Bans.Find(Ban => Action.User.ID == Ban.User.ID) is ActionModel Ban)
            {
                Shared.Bans.Remove(Ban);
                Json.SerializeToFile(Shared.Bans, "Actions/Bans");
            }
            if (Shared.TempBans.Find(TempBan => Action.User.ID == TempBan.User.ID) is ActionModel TempBan)
            {
                Shared.TempBans.Remove(TempBan);
                Json.SerializeToFile(Shared.TempBans, "Actions/TempBans");
            }
            Log.Info($"User unbanned - {Action}", ConsoleColor.Yellow, "Logs");
            return true; 
        }
        catch { return false; }
    }

    public static async Task<bool> TempBan(DiscordMember User, ActionModel Action)
    {
        try
        {
            await User.SendMessageAsync(Discord.Builder($"Damn bro u got tempbanned from 'IcyCord' lol.", Description: Action.ToDiscordString()));
        }
        catch { Log.Warning("Could not send direct message user who got tempbanned!"); }

        try
        {
            await User.BanAsync(reason: Action.Reason);
            Shared.TempBans.Add(Action);
            Json.SerializeToFile(Shared.TempBans, "Actions/TempBans");
            Log.Info($"User tempbanned - {Action}", ConsoleColor.Yellow, "Logs");
            return true;
        }
        catch { return false; }
    }

    public static async Task<bool> Mute(DiscordMember User, ActionModel Action)
    {
        try
        {
            if (Discord.HasRole(User, Shared.Config.Roles.Muted)) return false;

            await User.GrantRoleAsync(Discord.GetRole(Shared.Config.Roles.Muted), Action.Reason);
            Shared.Mutes.Add(Action);
            Json.SerializeToFile(Shared.Mutes, "Actions/Mutes");
            Log.Info($"User muted - {Action}", ConsoleColor.Yellow, "Logs");
            return true;
        }
        catch { return false; }
    }

    public static async Task<bool> Unmute(DiscordMember User, ActionModel Action)
    {
        try
        {
            if (!Discord.HasRole(User, Shared.Config.Roles.Muted)) return false;

            await User.RevokeRoleAsync(Discord.GetRole(Shared.Config.Roles.Muted), Action.Reason);
            if (Shared.Mutes.Find(Mute => Action.User.ID == Mute.User.ID) is ActionModel Mute)
            {
                Shared.Mutes.Remove(Mute);
                Json.SerializeToFile(Shared.Mutes, "Actions/Mutes");
            }
            if (Shared.TempMutes.Find(TempMute => Action.User.ID == TempMute.User.ID) is ActionModel TempMute)
            {
                Shared.TempMutes.Remove(TempMute);
                Json.SerializeToFile(Shared.TempMutes, "Actions/TempMutes");
            }
            Log.Info($"User unmuted - {Action}", ConsoleColor.Yellow, "Logs");
            return true;
        }
        catch { return false; }
    }

    public static async Task<bool> TempMute(DiscordMember User, ActionModel Action)
    {
        try
        {
            if (Discord.HasRole(User, Shared.Config.Roles.Muted)) return false;

            await User.GrantRoleAsync(Discord.GetRole(Shared.Config.Roles.Muted), Action.Reason);
            Shared.TempMutes.Add(Action);
            Json.SerializeToFile(Shared.TempMutes, "Actions/TempMutes");
            Log.Info($"User tempmuted - {Action}", ConsoleColor.Yellow, "Logs");
            return true;
        }
        catch { return false; }
    }

    public static async Task<bool> Invisible(DiscordMember User, ActionModel Action)
    {
        try
        {
            if (Discord.HasRole(User, Shared.Config.Roles.Invisible)) return false;

            await User.GrantRoleAsync(Discord.GetRole(Shared.Config.Roles.Invisible), Action.Reason);
            Shared.Invisibles.Add(Action);
            Json.SerializeToFile(Shared.Invisibles, "Actions/Invisibles");
            Log.Info($"User invisibled - {Action}", ConsoleColor.Yellow, "Logs");
            return true;
        }
        catch { return false; }
    }

    public static async Task<bool> Uninvisible(DiscordMember User, ActionModel Action)
    {
        try
        {
            if (!Discord.HasRole(User, Shared.Config.Roles.Invisible)) return false;

            await User.RevokeRoleAsync(Discord.GetRole(Shared.Config.Roles.Invisible), Action.Reason);
            if (Shared.Invisibles.Find(Invisible => Action.User.ID == Invisible.User.ID) is ActionModel Invisible)
            {
                Shared.Invisibles.Remove(Invisible);
                Json.SerializeToFile(Shared.Invisibles, "Actions/Invisibles");
            }
            if (Shared.TempInvisibles.Find(TempInvisible => Action.User.ID == TempInvisible.User.ID) is ActionModel TempInvisible)
            {
                Shared.TempInvisibles.Remove(TempInvisible);
                Json.SerializeToFile(Shared.TempInvisibles, "Actions/TempInvisibles");
            }
            Log.Info($"User uninvisibled - {Action}", ConsoleColor.Yellow, "Logs");
            return true;
        }
        catch { return false; }
    }

    public static async Task<bool> TempInvisible(DiscordMember User, ActionModel Action)
    {
        try
        {
            if (Discord.HasRole(User, Shared.Config.Roles.Invisible)) return false;

            await User.GrantRoleAsync(Discord.GetRole(Shared.Config.Roles.Invisible), Action.Reason);
            Shared.TempInvisibles.Add(Action);
            Json.SerializeToFile(Shared.TempInvisibles, "Actions/TempInvisibles");
            Log.Info($"User tempinvisibled - {Action}", ConsoleColor.Yellow, "Logs");
            return true;
        }
        catch { return false; }
    }

    public static async Task<bool> Kick(DiscordMember User, EntityModel By, string Reason = "N/A")
    {
        try
        {
            await User.RemoveAsync(Reason);
            Log.Info($"User kicked - User: {User.Username}, By: {By.Name}, Reason: {Reason}", ConsoleColor.Yellow, "Logs");
            return true;
        }
        catch { return false; }
    }

    public static async Task<bool> Clear(DiscordChannel Channel, int Amount)
    {
        if (Amount < 1) return false;

        try
        {
            await Channel.DeleteMessagesAsync(await Channel.GetMessagesAsync(Amount + 1));
            Log.Info($"Messages cleared - Channel: {Channel}, Amount: {Amount}", ConsoleColor.Yellow, "Logs");
            return true;
        }
        catch { return false; }
    }

    public static bool Warn(EntityModel User, EntityModel By, string Reason = "N/A")
    {
        try
        {
            if (Shared.Warnings.Find(Warning => Warning.User.ID == User.ID) is WarningModel Warning)
                Warning.Inner.Add(new(By, Reason, DateTime.UtcNow));
            else
                Shared.Warnings.Add(new(User, new() { new(By, Reason, DateTime.UtcNow) }));
            Json.SerializeToFile(Shared.Warnings, "Warnings");
            Log.Info($"User warned - User: {User.Name}, By: {By.Name}, Reason: {Reason}", ConsoleColor.Yellow, "Logs");
            return true;
        }
        catch { return false; }
    }

    public static bool ClearWarnings(EntityModel User, EntityModel By)
    {
        try
        {
            if (Shared.Warnings.Find(Warning => Warning.User.ID == User.ID) is WarningModel Warning)
            {
                Shared.Warnings.Remove(Warning);
                Json.SerializeToFile(Shared.Warnings, "Warnings");
                Log.Info($"Warnings cleared - User: {User.Name}, By: {By.Name}", ConsoleColor.Yellow, "Logs");
                return true;
            }
            else
                return false;
        }
        catch { return false; }
    }

    public static SnipeModel? Snipe(ulong Channel) =>
        Shared.Snipes.Find(Snipe => Snipe.Channel.ID == Channel) is SnipeModel Snipe ? Snipe : null;

    public static SnipeModel? ESnipe(ulong Channel) =>
        Shared.ESnipes.Find(ESnipe => ESnipe.Channel.ID == Channel) is SnipeModel ESnipe ? ESnipe : null;

    public static Stream? Backup()
    {
        try
        {
            Log.Info($"Backup requested", ConsoleColor.Yellow, "Logs");
            return Local.Zip("FIX THIS SHIT");
        }
        catch { return null; }
    }
}