using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.Controllers.Common;
using Imager.ImageResizerAggregator.Server.Services.Interfaces;

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

    [HttpPost("test-email/{email}")]
    public async Task<ActionResult> TestEmail(IFormFile file, string email, [FromServices] IEmailService emailService)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        await emailService.SendOrderConfirmationAsync(ms.ToArray(), Path.GetFileName(file.FileName).Trim('.'), email);
        return Ok();
    }
}