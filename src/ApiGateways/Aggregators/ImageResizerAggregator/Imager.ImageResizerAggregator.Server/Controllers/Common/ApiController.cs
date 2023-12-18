using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerAggregator.Server.Controllers.Common;

[Authorize]
[ApiController]
public class ApiController : ControllerBase
{

}