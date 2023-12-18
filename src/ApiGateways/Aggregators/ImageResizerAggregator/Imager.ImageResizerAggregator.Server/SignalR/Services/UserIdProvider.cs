using Imager.ImageResizerAggregator.Server.Authentication.Constants;

using Microsoft.AspNetCore.SignalR;

namespace Imager.ImageResizerAggregator.Server.SignalR.Services;

public class UserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User.Claims.FirstOrDefault(x => x.Type.Equals(UserClaimTypes.Id, StringComparison.Ordinal))?.Value;
    }
}