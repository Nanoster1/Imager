namespace Imager.Dapr.Events;

public record OnResizeImageEvent(string ImageId, string UserId, int Width, int Height, string ImageFormat);