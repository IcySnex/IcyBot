namespace IcyBot.Logic.Models
{
    public class WarningModel
    {
        public WarningModel() { }
        public WarningModel(EntityModel User, List<WarningInnerModel> Inner)
        {
            this.User = User;
            this.Inner = Inner;
        }

        public EntityModel User { get; set; } = new();
        public List<WarningInnerModel> Inner { get; set; } = new();
    }

    public class WarningInnerModel
    {
        public WarningInnerModel() { }
        public WarningInnerModel(EntityModel By, string Reason, DateTime DateTime)
        {
            this.By = By;
            this.Reason = Reason;
            this.DateTime = DateTime;
        }

        public EntityModel By { get; set; } = new();
        public string Reason { get; set; } = "N/A";
        public DateTime DateTime { get; set; }
    }
}
