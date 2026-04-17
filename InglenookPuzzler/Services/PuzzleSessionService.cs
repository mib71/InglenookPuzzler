using InglenookPuzzler.Data;
using InglenookPuzzler.Models.Puzzle;
using Microsoft.EntityFrameworkCore;

namespace InglenookPuzzler.Services;

public class PuzzleSessionService(AppDbContext db)
{
    private readonly AppDbContext _db = db;

    public async Task SaveAsync(PuzzleSession session)
    {
        _db.PuzzleSessions.Add(session);
        await _db.SaveChangesAsync();
    }

    public async Task<List<PuzzleSession>> GetCompletedAsync()
        => await _db.PuzzleSessions
            .Where(s => s.IsCompleted)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

    public async Task<PuzzleSession?> GetBestAsync()
        => await _db.PuzzleSessions
            .Where(s => s.IsCompleted)
            .OrderBy(s => s.MoveCount)
            .FirstOrDefaultAsync();

    public async Task<int> GetCurrentStreakAsync()
    {
        var sessions = await _db.PuzzleSessions
            .Where(s => s.IsCompleted)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

        if (!sessions.Any()) return 0;

        var today = DateTime.UtcNow.Date;
        var streak = 0;
        var checkDate = today;

        // If no session today, start checking from yesterday
        if (sessions.First().CreatedAt.Date < today)
            checkDate = today.AddDays(-1);

        foreach (var date in sessions.Select(s => s.CreatedAt.Date).Distinct().OrderByDescending(d => d))
        {
            if (date == checkDate)
            {
                streak++;
                checkDate = checkDate.AddDays(-1);
            }
            else break;
        }

        return streak;
    }
}