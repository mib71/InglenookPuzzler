namespace InglenookPuzzler.Models.Puzzle;

public class TrackState
{
    public string TrackId { get; set; } = string.Empty;  // "A", "B", "C"
    public int Capacity { get; set; }
    public List<int> WagonIds { get; set; } = new List<int>();
}
