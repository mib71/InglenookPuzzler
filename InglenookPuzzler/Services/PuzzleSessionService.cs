using InglenookPuzzler.Data;
using InglenookPuzzler.Models.Puzzle;
using Microsoft.EntityFrameworkCore;

namespace InglenookPuzzler.Services;

public class PuzzleSessionService
{
    private readonly AppDbContext _db;

    public PuzzleSessionService(AppDbContext db)
    {
        _db = db;
    }

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
}