using InglenookPuzzler.Data;
using InglenookPuzzler.Models;
using Microsoft.EntityFrameworkCore;

namespace InglenookPuzzler.Services;

public class EraService(AppDbContext db)
{
    private readonly AppDbContext _db = db;

    public async Task<List<Era>> GetAllAsync()
        => await _db.Eras.OrderBy(e => e.Name).ToListAsync();

    public async Task<Era?> GetByIdAsync(int id)
        => await _db.Eras.FindAsync(id);

    public async Task AddAsync(Era era)
    {
        _db.Eras.Add(era);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Era era)
    {
        _db.Eras.Update(era);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var era = await _db.Eras.FindAsync(id);
        if (era is not null)
        {
            _db.Eras.Remove(era);
            await _db.SaveChangesAsync();
        }
    }
}
