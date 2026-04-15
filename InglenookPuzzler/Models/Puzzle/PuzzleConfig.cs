namespace InglenookPuzzler.Models.Puzzle;

public class PuzzleConfig
{
    public int TrackACapacity { get; set; } = 5;
    public int TrackBCapacity { get; set; } = 3;
    public int TrackCCapacity { get; set; } = 3;
    public int HeadshuntCapacity { get; set; } = 3;
    public int TotalWagons { get; set; } = 8;
    public int GoalWagons { get; set; } = 5;
    public bool TimedMode { get; set; } = false;
}
