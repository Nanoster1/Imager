namespace Imager.ImageResizerAggregator.Contracts.Routes;

public static partial class HttpRoutes
{
    public const string Prefix = "/api";
    public const string ExceptionHandler = $"{Prefix}/error";
    public const string ResizeImageHub = $"{Prefix}/hub/resize";
    public const string ImageResizeController = $"{Prefix}/resize";
    public const string ImageController = $"{Prefix}/images";
}