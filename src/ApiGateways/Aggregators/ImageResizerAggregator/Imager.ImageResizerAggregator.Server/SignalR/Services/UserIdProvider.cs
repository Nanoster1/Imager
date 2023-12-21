using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.SignalR;

namespace Imager.ImageResizerAggregator.Server.SignalR.Services;

public class UserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Sub, StringComparison.Ordinal))?.Value;
    }
}