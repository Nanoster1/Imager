namespace Imager.ImageResizerAggregator.Server.Configuration.Settings;

public record FilesSettings(int LengthLimit)
{
    public const string SectionName = "Files";
}