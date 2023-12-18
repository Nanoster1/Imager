using Imager.ImageResizerAggregator.Server.Controllers.Common;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerAggregator.Server.Controllers;

[Route("/resize")]
public class ResizeImageController : ApiController
{
    [HttpPost]
    public ActionResult ResizeImage()
    {
        return Ok(User.Claims.Select(x => new { x.Type, x.Value }));
    }
}