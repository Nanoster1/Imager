namespace Imager.ImageResizerAggregator.Server.SignalR.Clients;

public interface IResizeImageClient
{
    Task SendResizedImageInfo(string imageId);
}