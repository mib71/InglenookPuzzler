using InglenookPuzzler.Data;
using InglenookPuzzler.Services;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.Run();
