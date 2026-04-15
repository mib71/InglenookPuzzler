using InglenookPuzzler.Models.Puzzle;
using System.Text.Json;

namespace InglenookPuzzler.Services;

public class PuzzleEngine
{
    public List<TrackState> Tracks { get; private set; } = new();
    public List<TrackState> GoalTracks { get; private set; } = new();
    public PuzzleConfig Config { get; private set; } = new();
    public int MoveCount { get; private set; }
    public bool IsCompleted { get; private set; }

    public void LoadSession(PuzzleSession session)
    {
        Tracks = JsonSerializer.Deserialize<List<TrackState>>(session.StartState)!;
        GoalTracks = JsonSerializer.Deserialize<List<TrackState>>(session.GoalState)!;
        MoveCount = session.MoveCount;
        IsCompleted = session.IsCompleted;
        Config = session.Config;
    }

    // A move is only valid if it goes via H
    // A/B/C → H or H → A/B/C
    public bool CanMove(string fromTrackId, string toTrackId)
    {
        var from = GetTrack(fromTrackId);
        var to = GetTrack(toTrackId);

        if (from is null || to is null) return false;
        if (from.WagonIds.Count == 0) return false;
        if (to.WagonIds.Count >= to.Capacity) return false;

        // Must always go via H
        var validMove = (fromTrackId == "H" && toTrackId != "H") ||
                        (fromTrackId != "H" && toTrackId == "H");

        return validMove;
    }

    public bool TryMove(string fromTrackId, string toTrackId)
    {
        if (!CanMove(fromTrackId, toTrackId)) return false;

        var from = GetTrack(fromTrackId)!;
        var to = GetTrack(toTrackId)!;

        // Always take from the end (buffer stop end)
        // and add to the end of the destination
        var wagonId = from.WagonIds.Last();
        from.WagonIds.RemoveAt(from.WagonIds.Count - 1);
        to.WagonIds.Add(wagonId);

        MoveCount++;
        IsCompleted = CheckWin();

        return true;
    }

    public bool TryMoveMultiple(string fromTrackId, string toTrackId, int count)
    {
        var from = GetTrack(fromTrackId);
        var to = GetTrack(toTrackId);

        if (from is null || to is null) return false;
        if (from.WagonIds.Count < count) return false;
        if (to.WagonIds.Count + count > to.Capacity) return false;

        var validMove = (fromTrackId == "H" && toTrackId != "H") ||
                        (fromTrackId != "H" && toTrackId == "H");
        if (!validMove) return false;

        // Take from loco end of H (first elements)
        // Take from buffer end of siding (last elements)
        var group = fromTrackId == "H"
            ? from.WagonIds.Take(count).ToList()
            : from.WagonIds.Skip(from.WagonIds.Count - count).ToList();

        // Remove from source
        if (fromTrackId == "H")
            from.WagonIds.RemoveRange(0, count);
        else
            from.WagonIds.RemoveRange(from.WagonIds.Count - count, count);

        // Add to front of H or back of siding
        if (toTrackId == "H")
            to.WagonIds.InsertRange(0, group);
        else
            to.WagonIds.AddRange(group);

        MoveCount++;
        IsCompleted = CheckWin();

        return true;
    }

    public bool CheckWin()
    {
        var trackA = GetTrack("A");
        var goalA = GoalTracks.FirstOrDefault(t => t.TrackId == "A");

        if (trackA is null || goalA is null) return false;

        return trackA.WagonIds.SequenceEqual(goalA.WagonIds);
    }

    public TrackState? GetTrack(string trackId)
        => Tracks.FirstOrDefault(t => t.TrackId == trackId);

    public List<int> GetGoalWagonIds()
        => GoalTracks.FirstOrDefault(t => t.TrackId == "A")?.WagonIds ?? new();
}