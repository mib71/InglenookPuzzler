using InglenookPuzzler.Data;
using InglenookPuzzler.Models;
using Microsoft.EntityFrameworkCore;

namespace InglenookPuzzler.Services;

public class WagonService(AppDbContext db)
{
    private readonly AppDbContext _db = db;

    public async Task<List<Wagon>> GetAllAsync()
        => await _db.Wagons
            .Include(w => w.WagonType)
            .Include(w => w.Era)
            .OrderBy(w => w.Name)
            .ToListAsync();

    public async Task<Wagon?> GetByIdAsync(int id)
        => await _db.Wagons
            .Include(w => w.WagonType)
            .Include(w => w.Era)
            .FirstOrDefaultAsync(w => w.Id == id);

    public async Task AddAsync(Wagon wagon)
    {
        _db.Wagons.Add(wagon);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Wagon wagon)
    {
        _db.Wagons.Update(wagon);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var wagon = await _db.Wagons.FindAsync(id);
        if (wagon is not null)
        {
            _db.Wagons.Remove(wagon);
            await _db.SaveChangesAsync();
        }
    }
}
