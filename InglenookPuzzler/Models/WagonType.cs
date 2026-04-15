namespace InglenookPuzzler.Models;

public class WagonType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();
}
