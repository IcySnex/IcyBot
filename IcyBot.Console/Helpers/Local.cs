﻿namespace IcyBot.Console.Helpers
{
    public class Local
    {
        public static string GetPath(string Path) =>
            $"{AppDomain.CurrentDomain.BaseDirectory}Database\\{Path}";
    }
}
