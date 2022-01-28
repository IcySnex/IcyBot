namespace IcyBot.Logic.Models
{
    public class SnipeModel
    {
        public SnipeModel() { }
        public SnipeModel(EntityModel Channel, EntityModel User, ulong ID, string Content, string[] AttachmentUrls, DateTime DateTime, string ContentAft = "*N/A*")
        {
            this.Channel = Channel;
            this.User = User;
            this.ID = ID;
            this.Content = Content;
            this.AttachmentUrls = AttachmentUrls;
            this.DateTime = DateTime;
            this.ContentAft = ContentAft;
        }

        public EntityModel Channel { get; set; } = new();
        public EntityModel User { get; set; } = new();
        public ulong ID { get; set; }
        public string Content { get; set; } = "*None*";
        public string[] AttachmentUrls { get; set; } = Array.Empty<string>();
        public DateTime DateTime { get; set; }
        public string ContentAft { get; set; } = "*N/A*";
    }
}
