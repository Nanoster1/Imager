namespace Imager.Dapr.Events;

public record OnImageResizedEvent(string UserId, string ResizedImageId);