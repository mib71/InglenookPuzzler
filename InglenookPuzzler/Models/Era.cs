using System.ComponentModel.DataAnnotations;

namespace InglenookPuzzler.Models;

public class Era
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(10, ErrorMessage = "Name must be 10 characters or less")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "Description must be 50 characters or less")]
    public string? Description { get; set; }

    public ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();
}