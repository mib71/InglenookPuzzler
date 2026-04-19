using System.ComponentModel.DataAnnotations;

namespace InglenookPuzzler.Models;

public class WagonType
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(20, ErrorMessage = "Name must be 20 characters or less")]
    public string Name { get; set; } = string.Empty;
    public ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();
}
