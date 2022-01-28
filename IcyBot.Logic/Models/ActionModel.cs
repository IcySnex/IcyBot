namespace IcyBot.Logic.Models;

public class ActionModel
{
    public ActionModel() { }
    public ActionModel(EntityModel User, EntityModel By, string Reason, DateTime DateTime, DateTime? EndDateTime)
    {
        this.User = User;
        this.By = By;
        this.Reason = Reason;
        this.DateTime = DateTime;
        this.EndDateTime = EndDateTime;
    }

    public EntityModel User { get; set; } = new();
    public EntityModel By { get; set; } = new();
    public string Reason { get; set; } = "N/A";
    public DateTime DateTime { get; set; }
    [Newtonsoft.Json.JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public DateTime? EndDateTime { get; set; }

    public override string ToString() =>
        $"User: {User.Name}, By: {By.Name}, Reason: {Reason}{(EndDateTime is null ? "" : $", Duration: {Text.FormatTime((EndDateTime - DateTime).Value)}")}";

    public string ToDiscordString(bool IncludeDuration = true) =>
        $"**Reason:** {Reason}{(IncludeDuration ? $"\n**Duration:** {(EndDateTime is null ? "Permanent" : Text.FormatTime((EndDateTime - DateTime).Value))}" : "")}\n**By:** <@!{By.ID}>, {Text.FormatDate(DateTime)}";
}