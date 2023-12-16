using Dapr.Client;

using Imager.ImageResizerService.Contracts.Routes;
using Imager.ImageResizerService.Server.Controllers.Common;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerService.Server.Controllers;

[Route(HttpRoutes.ImageResizeController.Prefix)]
public class ImageResizeController(DaprClient daprClient, ILogger<ImageResizeController> logger) : ApiController
{
    private readonly DaprClient _daprClient = daprClient;
    private readonly ILogger<ImageResizeController> _logger = logger;

    public Task ResizeImage()
    {
        return Task.CompletedTask;
    }
}