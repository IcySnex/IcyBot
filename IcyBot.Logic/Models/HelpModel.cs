namespace IcyBot.Logic.Models
{
    public class HelpModel
    {
        public HelpModel(HelpModelType Type, string Name, string Description, List<HelpModel>? List = null)
        {
            this.Type = Type;
            this.Name = Name;
            this.Description = Description;
            this.List = List == null ? new() : List;
        }

        public override string ToString() =>
            $"{Name.ToLower()} {string.Join(" ", List!.Select(List_ => List_.Name))}";

        public HelpModelType Type { get; }
        public string Name { get; }
        public string Description { get; }
        public List<HelpModel>? List { get; set; }
    }
}
