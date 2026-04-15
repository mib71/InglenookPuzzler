using InglenookPuzzler.Data;
using InglenookPuzzler.Models;
using Microsoft.EntityFrameworkCore;

namespace InglenookPuzzler.Services;

public class WagonTypeService(AppDbContext db)
{
    private readonly AppDbContext _db = db;

    public async Task<List<WagonType>> GetAllAsync()
        => await _db.WagonTypes.OrderBy(t => t.Name).ToListAsync();

    public async Task<WagonType?> GetByIdAsync(int id)
        => await _db.WagonTypes.FindAsync(id);

    public async Task AddAsync(WagonType wagonType)
    {
        _db.WagonTypes.Add(wagonType);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(WagonType wagonType)
    {
        _db.WagonTypes.Update(wagonType);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var wagonType = await _db.WagonTypes.FindAsync(id);
        if (wagonType is not null)
        {
            _db.WagonTypes.Remove(wagonType);
            await _db.SaveChangesAsync();
        }
    }
}