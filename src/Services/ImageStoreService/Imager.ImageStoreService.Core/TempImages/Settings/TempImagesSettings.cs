namespace Imager.ImageStoreService.Core.TempImages.Settings;

public class TempImagesSettings
{
    public const string SectionName = "TempImages";
    public required string PresignTTL { get; set; }
}