using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.Controllers.Common;
using Imager.ImageResizerAggregator.Server.Services.Interfaces;
using Imager.ImageStoreService.Contracts.HttpResponses;
using GetImageRequest = Imager.ImageResizerAggregator.Contracts.Requests.GetImageRequest;
using ImageServiceGetImageRequest = Imager.ImageStoreService.Contracts.HttpRequests.GetImageRequest;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerAggregator.Server.Controllers;

[Route(HttpRoutes.ImageController)]
public class ImageController(IImageService imageService) : ApiController
{
    private readonly IImageService _imageService = imageService;

    [Route("{request}")]
    public async Task<ActionResult<GetImageResponse>> GetImage(
        [FromRoute] GetImageRequest request,
        CancellationToken cancellationToken)
    {
        var user = GetUser();
        var getImageRequest = new ImageServiceGetImageRequest(user.Id, request.ImageId);
        var getImageResponse = await _imageService.GetImageAsync(getImageRequest, cancellationToken);
        if (getImageResponse is null) return NotFound();
        var response = new GetImageResponse(getImageResponse.ImageId, new(getImageResponse.Image.ImageInBytes, getImageResponse.Image.Format));
        return Ok(response);
    }
}