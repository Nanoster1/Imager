using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.Controllers.Common;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerAggregator.Server.Controllers;

[AllowAnonymous]
[Route(HttpRoutes.ExceptionHandler)]
public class ErrorController : ApiController
{
    [HttpGet]
    public ActionResult<ProblemDetails> Error()
    {
        return Problem();
    }
}