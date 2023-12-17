namespace Imager.ImageStoreService.Core.Common.Settings;

public class DaprSettings
{
    public const string SectionName = "Dapr";
    public required int DaprPort { get; set; }
}