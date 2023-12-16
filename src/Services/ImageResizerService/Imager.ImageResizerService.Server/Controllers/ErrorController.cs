using Imager.ImageResizerService.Contracts.Routes;
using Imager.ImageResizerService.Server.Controllers.Common;


using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerService.Server.Controllers;

[Route(HttpRoutes.ExceptionHandler)]
public class ErrorController : ApiController
{
    [HttpGet]
    public ActionResult<ProblemDetails> Error()
    {
        return Problem();
    }
}