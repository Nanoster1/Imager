using System.IdentityModel.Tokens.Jwt;


using Imager.ImageResizerAggregator.Server.Authentication.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerAggregator.Server.Controllers.Common;

[Authorize]
[ApiController]
public abstract class ApiController : ControllerBase
{
    protected User GetUser()
    {
        var userId = User.Claims.First(x => x.Type.Equals(JwtRegisteredClaimNames.Sub, StringComparison.Ordinal)).Value;
        return new User(userId);
    }
}