using Newtonsoft.Json;

namespace IcyBot.Logic.Helpers;

public class Json
{
    public static string Serialize(object Input) =>
        JsonConvert.SerializeObject(Input);

    public static void SerializeToFile(object Input, string Path) =>
        File.WriteAllText(Local.Path(Path + ".json"), Serialize(Input));

    public static T Deserialize<T>(string Input, bool IsFile = true, JsonSerializerSettings? Settings = null) =>
        Settings is null ?
        (JsonConvert.DeserializeObject<T>(IsFile ? File.ReadAllText(Local.Path(Input + ".json")) : Input) is T Result) ? Result : throw Exceptions.JsonReturnedInvalid :
        (JsonConvert.DeserializeObject<T>(IsFile ? File.ReadAllText(Local.Path(Input + ".json")) : Input, Settings) is T Result2) ? Result2 : throw Exceptions.JsonReturnedInvalid;

    public static bool TryDeserialize<T>(out T Result, string Input, bool IsFile = true)
    {
        bool su = true;
        Result = Deserialize<T>(Input, IsFile,
            new() { Error = (s, e) => { su = false; e.ErrorContext.Handled = true; }, MissingMemberHandling = MissingMemberHandling.Error });
        return su;
    }

    public static T Load<T>(string Input, T Default) => 
        File.Exists(Local.Path(Input + ".json")) ? Deserialize<T>(Input) : Default;
}