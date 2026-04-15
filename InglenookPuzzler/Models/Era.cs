namespace InglenookPuzzler.Models;

public class Era
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();
}
