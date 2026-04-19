using System.ComponentModel.DataAnnotations;

namespace InglenookPuzzler.Models;

public class Wagon
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(5, ErrorMessage = "Name must be 5 characters or less")]
    public string Name { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Please select a wagon type")]
    public int WagonTypeId { get; set; }
    public WagonType WagonType { get; set; } = null!;
    public int? EraId { get; set; }
    public Era? Era { get; set; }

    [MaxLength(12, ErrorMessage = "Rolling stock number must be 12 characters or less")]
    public string? RollingStockNumber { get; set; }  // e.g. "50 1234"

    [MaxLength(15, ErrorMessage = "Color must be 15 characters or less")]
    public string? Color { get; set; }
    public string? ImagePath { get; set; }

    [MaxLength(250, ErrorMessage = "Notes must be 250 characters or less")]
    public string? Notes { get; set; }
}
