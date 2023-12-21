using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.Controllers.Common;
using Imager.ImageResizerAggregator.Server.Services.Interfaces;
using GetImageRequest = Imager.ImageResizerAggregator.Contracts.Requests.GetImageRequest;
using GetImageResponse = Imager.ImageResizerAggregator.Contracts.Responses.GetImageResponse;
using ImageServiceGetImageRequest = Imager.ImageStoreService.Contracts.HttpRequests.GetImageRequest;

using Microsoft.AspNetCore.Mvc;
using Imager.ImageResizerAggregator.Contracts.Requests;
using Imager.ImageResizerAggregator.Contracts.Responses;
using Imager.ImageStoreService.Contracts.HttpRequests;

namespace Imager.ImageResizerAggregator.Server.Controllers;

[Route(HttpRoutes.ImageController)]
public class ImageController(IImageService imageService, IEmailService emailService) : ApiController
{
    private readonly IImageService _imageService = imageService;
    private readonly IEmailService _emailService = emailService;

    [HttpGet]
    public async Task<ActionResult<GetImageResponse>> GetImage(
        [FromQuery] GetImageRequest request,
        CancellationToken cancellationToken)
    {
        var user = GetUser();
        var getImageRequest = new ImageServiceGetImageRequest(user.Id, request.ImageId);
        var getImageResponse = await _imageService.GetImageAsync(getImageRequest, cancellationToken);
        if (getImageResponse is null) return NotFound();
        var response = new GetImageResponse(getImageResponse.ImageId, new(getImageResponse.Image.ImageInBytes, getImageResponse.Image.Format));
        return Ok(response);
    }

    [HttpGet("all")]
    public async Task<ActionResult> GetUserImages(CancellationToken cancellationToken)
    {
        var user = GetUser();
        var getUserImagesRequest = new GetUserImagesRequest(user.Id);
        var getUserImagesResponse = await _imageService.GetUserImagesAsync(getUserImagesRequest, cancellationToken);
        var response = new GetUserImagesResponse(getUserImagesResponse.ImageIds);
        return Ok(response);
    }

    [HttpPost("email")]
    public async Task<ActionResult> SendImageToEmail(
        [FromBody] SendImageToEmailRequest request,
        CancellationToken cancellationToken)
    {
        var user = GetUser();
        var getImageRequest = new ImageServiceGetImageRequest(user.Id, request.ImageId);
        var getImageResponse = await _imageService.GetImageAsync(getImageRequest, cancellationToken);
        await _emailService.SendOrderConfirmationAsync(getImageResponse.Image.ImageInBytes, getImageResponse.Image.Format, request.UserEmail);
        return NoContent();
    }
}