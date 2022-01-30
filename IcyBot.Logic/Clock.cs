namespace IcyBot.Logic
{
    public class Clock
    {
        private static Timer Timer = new(_ =>
        {
            CheckTempBans();
            CheckTempMutes();
            CheckTempInvsibles();
            CheckBackup();
            Log.Info("Elapsed", ConsoleColor.Cyan);
            Timer!.Change(Shared.Config.ClockInterval, -1);
        }, null, 1000, -1);
        public static void Start()
        {
            Timer.Change(1000, -1);
            Log.Info("Clock has started", ConsoleColor.Cyan);
        }

        public static async void CheckTempBans()
        {
            foreach(ActionModel TempBan in Shared.TempBans.ToList())
            {
                if (TempBan.EndDateTime is null) return;

                if (DateTime.UtcNow >= TempBan.EndDateTime.Value)
                    await Commands.Moderator.Unban(TempBan);
            }
        }

        public static async void CheckTempMutes()
        {
            foreach(ActionModel TempMute in Shared.TempMutes.ToList())
            {
                if (TempMute.EndDateTime is null) return;

                if (DateTime.UtcNow >= TempMute.EndDateTime.Value)
                    await Commands.Moderator.Unmute(await Discord.GetMember(TempMute.User.ID), TempMute);
            }
        }

        public static async void CheckTempInvsibles()
        {
            foreach(ActionModel TempInvisible in Shared.TempInvisibles.ToList())
            {
                if (TempInvisible.EndDateTime is null) return;

                if (DateTime.UtcNow >= TempInvisible.EndDateTime.Value)
                    await Commands.Moderator.Uninvisible(await Discord.GetMember(TempInvisible.User.ID), TempInvisible);
            }
        }

        public static async void CheckBackup()
        {
            if ((DateTime.UtcNow.Hour, DateTime.UtcNow.Minute) == (Shared.Config.Backup.Time.Hours, Shared.Config.Backup.Time.Minutes) && Shared.DiscordClient != null)
                await Discord.GetChannel(Shared.Config.Backup.Channel).SendMessageAsync(Commands.Moderator.Backup(true) is Stream Database ?
                    new DiscordMessageBuilder().WithEmbed(Discord.Builder("Here u have yo shit backuped")).WithFile("Database.zip", Database) :
                    new DiscordMessageBuilder().WithEmbed(Discord.Builder("Yo i couldnt backup yo shit!!", Color: Shared.Config.Colors.Error)));
        }
    }
}
