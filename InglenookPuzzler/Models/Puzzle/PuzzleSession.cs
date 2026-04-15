namespace InglenookPuzzler.Models.Puzzle;

public class PuzzleSession
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string StartState { get; set; } = string.Empty;  // JSON
    public string GoalState { get; set; } = string.Empty;   // JSON
    public int MoveCount { get; set; } = 0;
    public bool IsCompleted { get; set; } = false;

    public PuzzleConfig Config { get; set; } = new PuzzleConfig();
}
