namespace Imager.ImageResizerService.Contracts.Events;

public record ResizeImageEvent(string ImageId, string UserId, int Width, int Height, string ImageFormat);