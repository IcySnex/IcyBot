namespace IcyBot.Logic.Models;

public class EntityModel
{
    public EntityModel() { }
    public EntityModel(string Name, ulong ID)
    {
        this.Name = Name;
        this.ID = ID;
    }

    public string Name { get; set; } = "";
    public ulong ID { get; set; }
}
