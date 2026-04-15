using InglenookPuzzler.Models;
using InglenookPuzzler.Models.Puzzle;
using Microsoft.EntityFrameworkCore;

namespace InglenookPuzzler.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Wagon> Wagons => Set<Wagon>();
    public DbSet<WagonType> WagonTypes => Set<WagonType>();
    public DbSet<Era> Eras => Set<Era>();
    public DbSet<PuzzleSession> PuzzleSessions => Set<PuzzleSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PuzzleSession>()
            .OwnsOne(s => s.Config);

        modelBuilder.Entity<Wagon>()
            .HasOne(w => w.WagonType)
            .WithMany(t => t.Wagons)
            .HasForeignKey(w => w.WagonTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Wagon>()
            .HasOne(w => w.Era)
            .WithMany(e => e.Wagons)
            .HasForeignKey(w => w.EraId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
