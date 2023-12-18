namespace Imager.ImageResizerAggregator.Server.SignalR.Clients;

public interface IResizeImageClient
{
    Task SendResizedImage(string imageId, byte[] resizedImage);
}