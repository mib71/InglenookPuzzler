using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace InglenookPuzzler.Services;

public class ImageService(IWebHostEnvironment env)
{
    private readonly IWebHostEnvironment _env = env;

    public async Task<string> SaveImageAsync(Stream imageStream, string fileName)
    {
        var imagesFolder = Path.Combine(_env.WebRootPath, "images", "wagons");

        if (!Directory.Exists(imagesFolder))
            Directory.CreateDirectory(imagesFolder);

        var uniqueFileName = $"{Guid.NewGuid()}.jpg";
        var filePath = Path.Combine(imagesFolder, uniqueFileName);

        using var image = await Image.LoadAsync(imageStream);

        // Crop to 16:9 landscape
        var targetWidth = 640;
        var targetHeight = 360;

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(targetWidth, targetHeight),
            Mode = ResizeMode.Crop
        }));

        await image.SaveAsJpegAsync(filePath);

        return $"images/wagons/{uniqueFileName}";
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
