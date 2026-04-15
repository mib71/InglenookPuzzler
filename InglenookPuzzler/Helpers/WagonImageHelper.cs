using InglenookPuzzler.Models;

namespace InglenookPuzzler.Helpers;

public static class WagonImageHelper
{
    public static string GetImage(Wagon? wagon)
    {
        if (wagon is null) return "images/defaults/default.svg";
        if (!string.IsNullOrEmpty(wagon.ImagePath)) return wagon.ImagePath;

        return wagon.WagonType?.Name.ToLower() switch
        {
            "plank wagon" => "images/defaults/plank-wagon.png",
            "goods van" => "images/defaults/goods-van.png",
            "ventilated van" => "images/defaults/ventilated-van.png",
            "tank wagon" => "images/defaults/tank-wagon.png",
            "brake van" => "images/defaults/brake-van.png",
            "hopper wagon" => "images/defaults/hopper-wagon.png",
            "cattle wagon" => "images/defaults/cattle-wagon.png",
            "flat wagon" => "images/defaults/flat-wagon.png",
            "coal wagon" => "images/defaults/coal-wagon.png",
            _ => "images/defaults/default.png"
        };
    }
}
