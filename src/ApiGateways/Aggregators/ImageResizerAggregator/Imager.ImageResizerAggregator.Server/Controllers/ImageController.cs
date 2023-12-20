using Dapr;
using Dapr.Client;

using Imager.Dapr;

using Imager.Dapr.Events;
using Imager.ImageResizerAggregator.Contracts.Requests;
using Imager.ImageResizerAggregator.Contracts.Responses;
using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.Controllers.Common;
using Imager.ImageResizerAggregator.Server.Services.Interfaces;
using Imager.ImageResizerAggregator.Server.SignalR.Clients;
using Imager.ImageResizerAggregator.Server.SignalR.Hubs;
using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Imager.ImageResizerAggregator.Server.Controllers;

[Route(HttpRoutes.ImageController)]
public class ImageController(
    ITempImageService tempImageService,
    DaprClient daprClient,
    IImageService imageService) :
    ApiController
{
    private readonly ITempImageService _tempImageService = tempImageService;
    private readonly IImageService _imageService = imageService;
    private readonly DaprClient _daprClient = daprClient;

    [HttpPost]
    public async Task<ActionResult> StartResizeImages(
        [FromBody] StartResizeImagesRequest request,
        CancellationToken cancellationToken)
    {
        var user = GetUser();
        var tempImageFileModels = new List<TempImageFileModel>();
        foreach (var image in request.ImageModels)
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

    [HttpPost($"/{nameof(OnImageResizedEvent)}")]
    [Topic(DaprComponentsNames.ImagerPubsub, nameof(OnImageResizedEvent))]
    public async Task OnImageResized(
        [FromBody] OnImageResizedEvent @event,
        [FromServices] IHubContext<ResizeImageHub, IResizeImageClient> hub,
        CancellationToken cancellationToken)
    {
        var createImageRequest = new CreateImageFromTempRequest(@event.UserId, @event.ResizedImageId);
        var createImageResponse = await _imageService.CreateImageFromTempAsync(createImageRequest, cancellationToken);
        await hub.Clients.User(createImageResponse.UserId).SendResizedImageInfo(createImageResponse.ImageId);
    }
}