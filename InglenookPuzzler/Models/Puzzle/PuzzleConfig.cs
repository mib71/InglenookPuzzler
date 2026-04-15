namespace InglenookPuzzler.Models.Puzzle;

public class PuzzleConfig
{
    public int TrackACapacity { get; set; } = 3;
    public int TrackBCapacity { get; set; } = 2;
    public int TrackCCapacity { get; set; } = 2;
    public int WagonCount { get; set; } = 5;
    public bool TimedMode { get; set; } = false;
}
