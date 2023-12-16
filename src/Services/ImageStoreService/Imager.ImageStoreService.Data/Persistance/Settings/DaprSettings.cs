namespace Imager.ImageStoreService.Data.Persistance.Settings;

public class DaprSettings
{
    public const string SectionName = "Dapr";
    public required int DaprPort { get; set; }
}