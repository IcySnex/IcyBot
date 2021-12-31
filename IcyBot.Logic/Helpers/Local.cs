using System.Runtime.InteropServices;

namespace IcyBot.Logic.Helpers
{
    public class Local
    {
        public static string Separator = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\\" : "/";

        public static string GetPath(string Path) =>
            $"{AppDomain.CurrentDomain.BaseDirectory}Database{Separator}{Path}";
    }
}
