using InglenookPuzzler.Models;

namespace InglenookPuzzler.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!db.Eras.Any())
        {
            db.Eras.AddRange(
                new Era { Name = "I", Description = "Pioneer and Railway Boom (c. 1804–1874)" },
                new Era { Name = "II", Description = "Pre-Grouping (c. 1875–1922)" },
                new Era { Name = "III", Description = "The Big Four (c. 1923–1947)" }
            );
            await db.SaveChangesAsync();
        }

        if (!db.WagonTypes.Any())
        {
            db.WagonTypes.AddRange(
                new WagonType { Name = "Plank Wagon" },
                new WagonType { Name = "Goods Van" },
                new WagonType { Name = "Ventilated Van" },
                new WagonType { Name = "Tank Wagon" },
                new WagonType { Name = "Brake Van" },
                new WagonType { Name = "Hopper Wagon" },
                new WagonType { Name = "Cattle Wagon" },
                new WagonType { Name = "Flat Wagon" },
                new WagonType { Name = "Coal Wagon" }
            );
            await db.SaveChangesAsync();
        }
    }
}