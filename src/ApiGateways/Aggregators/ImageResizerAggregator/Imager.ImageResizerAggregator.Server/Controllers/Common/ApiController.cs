using Imager.ImageResizerAggregator.Server.Authentication.Constants;
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
        var userId = User.Claims.First(x => x.Type.Equals(UserClaimTypes.Id, StringComparison.Ordinal)).Value;
        return new User(userId);
    }
}