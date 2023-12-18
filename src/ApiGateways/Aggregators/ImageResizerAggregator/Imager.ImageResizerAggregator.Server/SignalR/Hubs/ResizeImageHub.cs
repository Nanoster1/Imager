using Imager.ImageResizerAggregator.Server.SignalR.Clients;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.SignalR;

namespace Imager.ImageResizerAggregator.Server.SignalR.Hubs;

[Authorize]
public class ResizeImageHub : Hub<IResizeImageClient>
{

}