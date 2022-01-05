using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IcyBot.Logic.Helpers;

public class Local
{
    public static bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? true : false;
    public static string OsInformation = RuntimeInformation.OSDescription;
    public static string Sl = IsWindows ? "\\" : "/";

    public static T RunSync<T>(Task<T> AsyncTask)
    {
        AsyncTask.Wait();
        return AsyncTask.Result;
    }

    public static string Path(string Path) =>
        $"{AppDomain.CurrentDomain.BaseDirectory}Database{Sl}{Path}";

    public static TimeSpan RunningFor() =>
        DateTime.Now - Process.GetCurrentProcess().StartTime;

    public static Task<string> IP() =>
        Web.String("https://api.ipify.org");

    public static double RamUsage()
    {
        double Usage = Process.GetCurrentProcess().PrivateMemorySize64 / 1048576d;
        return IsWindows ? Usage : Usage / 2;
    }

    public static async Task<double> CpuUsage()
    {
        var StartTime = DateTime.Now;
        var StartCpu = Process.GetCurrentProcess().TotalProcessorTime;
        await Task.Delay(500);
        return ((Process.GetCurrentProcess().TotalProcessorTime - StartCpu).TotalMilliseconds / (Environment.ProcessorCount * (DateTime.Now - StartTime).TotalMilliseconds) * 100);
    }
}
