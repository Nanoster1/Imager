using Imager.ImageStoreService.Contracts.Routes;
using Imager.ImageStoreService.Server.Controllers.Common;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageStoreService.Server.Controllers;

[Route(HttpRoutes.ExceptionHandler)]
public class ErrorController : ApiController
{
    public ActionResult<ProblemDetails> Error()
    {
        return Problem();
    }
}