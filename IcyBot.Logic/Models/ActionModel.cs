namespace IcyBot.Logic.Models;

public class ActionModel
{
    public ActionModel() { }
    public ActionModel(EntityModel User, EntityModel By, string Reason, DateTime DateTime, TimeSpan? Duration)
    {
        this.User = User;
        this.By = By;
        this.Reason = Reason;
        this.DateTime = DateTime;
        this.Duration = Duration;
    }

    public EntityModel User { get; set; } = new("", 0);
    public EntityModel By { get; set; } = new("", 0);
    public string Reason { get; set; } = "N/A";
    public DateTime DateTime { get; set; }
    public TimeSpan? Duration { get; set; }

    public override string ToString() =>
        $"User: {User.Name}, By: {By.Name}, Reason: {Reason}, DateTime: {DateTime}{(Duration is null ? "" : $", Duration: {Text.FormatTime(Duration.Value)}")}";

    public string ToDiscordString(bool IncludeDuration = true) =>
        $"**Reason:** {Reason}{(IncludeDuration ? $"\n**Duration:** {(Duration is null ? "Permanent" : Text.FormatTime(Duration.Value))}" : "")}\n**By:** <@!{By.ID}>, {Text.FormatDate(DateTime)}";
}