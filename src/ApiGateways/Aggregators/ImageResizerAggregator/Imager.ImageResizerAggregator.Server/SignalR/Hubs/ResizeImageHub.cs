using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.SignalR;

namespace Imager.ImageResizerAggregator.Server.SignalR.Hubs;

[Authorize]
public class ResizeImageHub(ILogger<ResizeImageHub> logger) : Hub
{
    private readonly ILogger<ResizeImageHub> _logger = logger;

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("{d}", "Asdasd");
        return base.OnConnectedAsync();
    }
}