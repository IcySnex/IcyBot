using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace IcyBot.Logic.Helpers;

public class Local
{
    public static bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? true : false;
    public static string OsInformation = RuntimeInformation.OSDescription;
    public static char Sl = IsWindows ? '\\' : '/';
    public static DateTime StartTime = Process.GetCurrentProcess().StartTime.ToUniversalTime();

    public static T RunSync<T>(Task<T> AsyncTask)
    {
        AsyncTask.Wait();
        return AsyncTask.Result;
    }

    public static string Path(string Path) =>
        $"{AppDomain.CurrentDomain.BaseDirectory}Database{Sl}{Path.Replace('/', Sl).Replace('\\', Sl)}";

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
        return (Process.GetCurrentProcess().TotalProcessorTime - StartCpu).TotalMilliseconds / (Environment.ProcessorCount * (DateTime.Now - StartTime).TotalMilliseconds) * 100;
    }

    public static MemoryStream Zip(string Input)
    {
        Log.Info(Path(Input));
        using (var Result = new MemoryStream())
        {
            using (var Zip = new ZipArchive(Result, ZipArchiveMode.Create, true))
                Zip.CreateEntryFromFile(Path(Input), Input.Split(Sl).Last());

            Result.Seek(0, SeekOrigin.Begin);
            return new(Result.ToArray());
        }
    }
}
