namespace InglenookPuzzler.Models;

public class Wagon
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int WagonTypeId { get; set; }
    public WagonType WagonType { get; set; } = null!;
    public int? EraId { get; set; }
    public Era? Era { get; set; }
    public string? Color { get; set; }
    public string? ImagePath { get; set; }
    public string? Notes { get; set; }
}
