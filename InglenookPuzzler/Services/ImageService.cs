namespace InglenookPuzzler.Services;

public class ImageService(IWebHostEnvironment env)
{
    private readonly IWebHostEnvironment _env = env;

    public async Task<string> SaveImageAsync(Stream imageStream, string fileName)
    {
        var imagesFolder = Path.Combine(_env.WebRootPath, "images");

        if (!Directory.Exists(imagesFolder))
            Directory.CreateDirectory(imagesFolder);

        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        var filePath = Path.Combine(imagesFolder, uniqueFileName);

        using var fileStream = File.Create(filePath);
        await imageStream.CopyToAsync(fileStream);

        return $"images/{uniqueFileName}";
    }

    public void DeleteImage(string? imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
            return;

        var fullPath = Path.Combine(_env.WebRootPath, imagePath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
