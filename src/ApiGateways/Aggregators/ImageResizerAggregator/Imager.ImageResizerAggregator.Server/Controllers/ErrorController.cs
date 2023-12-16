using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.Controllers.Common;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerAggregator.Server.Controllers;

[Route(HttpRoutes.ExceptionHandler)]
public class ErrorController : ApiController
{
    [HttpGet]
    public ActionResult<ProblemDetails> Error()
    {
        return Problem();
    }
}