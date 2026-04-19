using InglenookPuzzler.Data;
using InglenookPuzzler.Models;
using Microsoft.EntityFrameworkCore;

namespace InglenookPuzzler.Services;

public class WagonService(AppDbContext db, ImageService imageService)
{
    private readonly AppDbContext _db = db;
    private readonly ImageService _imageService = imageService;

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
        var existing = await _db.Wagons.FindAsync(wagon.Id);
        if (existing is null) return;

        existing.Name = wagon.Name;
        existing.WagonTypeId = wagon.WagonTypeId;
        existing.EraId = wagon.EraId;
        existing.Color = wagon.Color;
        existing.RollingStockNumber = wagon.RollingStockNumber;
        existing.ImagePath = wagon.ImagePath;
        existing.Notes = wagon.Notes;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var wagon = await _db.Wagons.FindAsync(id);
        if (wagon is not null)
        {
            _imageService.DeleteImage(wagon.ImagePath);
            _db.Wagons.Remove(wagon);
            await _db.SaveChangesAsync();
        }
    }

    public async Task GenerateSampleWagonsAsync()
    {
        var wagonTypes = await _db.WagonTypes.ToListAsync();
        var era = await _db.Eras.FirstOrDefaultAsync();

        if (!wagonTypes.Any()) return;

        // Sample wagons based on typical British Era I-III yards
        var samples = new[]
        {
            ("GWR", "Plank Wagon"),
            ("GWR", "Goods Van"),
            ("LMS", "Tank Wagon"),
            ("LMS", "Plank Wagon"),
            ("LNER", "Coal Wagon"),
            ("SR", "Cattle Wagon"),
            ("GWR", "Brake Van"),
            ("LMS", "Hopper Wagon")
        };

        foreach (var (name, typeName) in samples)
        {
            var wagonType = wagonTypes.FirstOrDefault(t =>
                t.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase))
                ?? wagonTypes.First();

            _db.Wagons.Add(new Wagon
            {
                Name = name,
                WagonTypeId = wagonType.Id,
                EraId = era?.Id
            });
        }

        await _db.SaveChangesAsync();
    }
}