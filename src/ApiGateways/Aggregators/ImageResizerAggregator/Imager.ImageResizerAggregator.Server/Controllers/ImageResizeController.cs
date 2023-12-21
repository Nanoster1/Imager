using Dapr;
using Dapr.Client;

using Dumpify;

using Imager.Dapr;

using Imager.Dapr.Events;
using Imager.ImageResizerAggregator.Contracts.Requests;
using Imager.ImageResizerAggregator.Contracts.Responses;
using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.Controllers.Common;
using Imager.ImageResizerAggregator.Server.Services.Interfaces;
using Imager.ImageResizerAggregator.Server.SignalR.Hubs;
using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.Models;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Imager.ImageResizerAggregator.Server.Controllers;

[Route(HttpRoutes.ImageResizeController)]
public class ImageResizeController(
    ITempImageService tempImageService,
    DaprClient daprClient,
    IImageService imageService) :
    ApiController
{
    private readonly ITempImageService _tempImageService = tempImageService;
    private readonly IImageService _imageService = imageService;
    private readonly DaprClient _daprClient = daprClient;

    [HttpPost]
    public async Task<ActionResult<StartResizeImagesResponse>> StartResizeImages(
        [FromBody] StartResizeImagesRequest request,
        CancellationToken cancellationToken)
    {
        var user = GetUser();
        var tempImageFileModels = new List<TempImageFileModel>();
        foreach (var image in request.Images)
        {
            tempImageFileModels.Add(new(image.ImageInBytes, image.Format));
        }
        var createTempImagesRequest = new CreateTempImagesRequest(user.Id, tempImageFileModels);
        var createTempImagesResponse = await _tempImageService.CreateTempImagesAsync(createTempImagesRequest, cancellationToken);

        foreach (var imageId in createTempImagesResponse.ImageIds)
        {
            var @event = new OnResizeImageEvent(imageId, user.Id, request.Width, request.Height);
            await _daprClient.PublishEventAsync(DaprComponentsNames.ImagerPubsub, nameof(OnResizeImageEvent), @event, cancellationToken);
        }

        var response = new StartResizeImagesResponse(createTempImagesResponse.ImageIds);
        return Ok(response);
    }

    [HttpPost($"/event")]
    [AllowAnonymous]
    [Topic(DaprComponentsNames.ImagerPubsub, nameof(OnImageResizedEvent))]
    public async Task OnImageResized(
        [FromBody] OnImageResizedEvent @event,
        [FromServices] IHubContext<ResizeImageHub> hub,
        [FromServices] ILogger<ImageResizeController> logger,
        CancellationToken cancellationToken)
    {
        var createImageRequest = new CreateImageFromTempRequest(@event.UserId, @event.ResizedImageId);
        var createImageResponse = await _imageService.CreateImageFromTempAsync(createImageRequest, cancellationToken);
        logger.LogInformation("{r}", hub.Clients.DumpText());
        await hub.Clients.User(createImageResponse.UserId).SendAsync("SendResizedImageInfo", createImageResponse.ImageId);
    }
}