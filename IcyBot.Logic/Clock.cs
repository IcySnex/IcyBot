namespace IcyBot.Logic
{
    public class Clock
    {
        private static Timer Timer = new(_ =>
        {
            CheckTempBans();
            CheckTempMutes();
            CheckTempInvsibles();
            //Log.Info("Elapsed", ConsoleColor.Cyan);
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

                if (DateTime.Compare(DateTime.UtcNow, TempBan.EndDateTime.Value) >= 0)
                    await Commands.Moderator.Unban(TempBan);
            }
        }

        public static async void CheckTempMutes()
        {
            foreach(ActionModel TempMute in Shared.TempMutes.ToList())
            {
                if (TempMute.EndDateTime is null) return;

                if (DateTime.Compare(DateTime.UtcNow, TempMute.EndDateTime.Value) >= 0)
                    await Commands.Moderator.Unmute(await Discord.GetMember(TempMute.User.ID), TempMute);
            }
        }

        public static async void CheckTempInvsibles()
        {
            foreach(ActionModel TempInvisible in Shared.TempInvisibles.ToList())
            {
                if (TempInvisible.EndDateTime is null) return;

                if (DateTime.Compare(DateTime.UtcNow, TempInvisible.EndDateTime.Value) >= 0)
                    await Commands.Moderator.Uninvisible(await Discord.GetMember(TempInvisible.User.ID), TempInvisible);
            }
        }
    }
}
