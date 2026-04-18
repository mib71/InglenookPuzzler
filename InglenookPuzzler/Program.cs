using InglenookPuzzler.Data;
using InglenookPuzzler.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var appData = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "InglenookPuzzler");
Directory.CreateDirectory(appData);
var dbPath = Path.Combine(appData, "inglenook.db");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<ImageService>();

builder.Services.AddScoped<WagonTypeService>();
builder.Services.AddScoped<EraService>();
builder.Services.AddScoped<WagonService>();
builder.Services.AddScoped<PuzzleGenerator>();
builder.Services.AddScoped<PuzzleEngine>();
builder.Services.AddScoped<PuzzleSessionService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Only lock to port 5000 in release — dev uses Visual Studio's port
if (!builder.Environment.IsDevelopment())
{
    builder.WebHost.UseUrls("http://localhost:5000");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapGet("/wagon-images/{fileName}", (string fileName) =>
{
    var folder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "InglenookPuzzler", "images", "wagons");

    var fullPath = Path.Combine(folder, fileName);

    if (!File.Exists(fullPath))
        return Results.NotFound();

    return Results.File(fullPath, "image/jpeg");
});

app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    await DbSeeder.SeedAsync(db);
}

// Only open browser automatically in release
if (!app.Environment.IsDevelopment())
{
    app.Lifetime.ApplicationStarted.Register(() =>
    {
        Process.Start(new ProcessStartInfo("http://localhost:5000")
        {
            UseShellExecute = true
        });
    });
}

app.Run();