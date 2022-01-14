namespace IcyBot.Logic
{
    public class Clock
    {
        private static Timer Timer = new(_ =>
        {
            CheckTempBans();
            Log.Info("Elapsed", ConsoleColor.Cyan, "Clock");
            Timer!.Change(Shared.Config.ClockInterval, -1);
        }, null, 1000, -1);

        public static void Start()
        {
            Timer.Change(1000, -1);

            Log.Info("Clock has started", ConsoleColor.Cyan, "Clock");
        }

        public static async void CheckTempBans()
        {
            foreach(ActionModel TempBan in Shared.TempBans.ToList())
            {
                if (TempBan.EndDateTime is null) return;
                Log.Warning(Json.Serialize(TempBan));
                if (DateTime.Compare(DateTime.UtcNow, TempBan.EndDateTime.Value) >= 0)
                    Log.Warning((await Commands.Moderator.Unban(TempBan.User.ID)).ToString());
            }
        }
    }
}
