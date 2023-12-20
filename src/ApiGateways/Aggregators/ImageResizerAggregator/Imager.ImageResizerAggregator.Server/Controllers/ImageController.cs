using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.Controllers.Common;
using Imager.ImageResizerAggregator.Server.Services.Interfaces;
using GetImageRequest = Imager.ImageResizerAggregator.Contracts.Requests.GetImageRequest;
using GetImageResponse = Imager.ImageResizerAggregator.Contracts.Responses.GetImageResponse;
using GetUserImagesResponse = Imager.ImageResizerAggregator.Contracts.Responses.GetUserImagesResponse;
using ImageServiceGetImageRequest = Imager.ImageStoreService.Contracts.HttpRequests.GetImageRequest;
using ImageServiceGetUserImagesRequest = Imager.ImageStoreService.Contracts.HttpRequests.GetUserImagesRequest;

using Microsoft.AspNetCore.Mvc;
using Imager.ImageResizerAggregator.Contracts.Models;
using Imager.ImageResizerAggregator.Contracts.Requests;

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
    public Task<ActionResult<GetImageResponse>> GetUserImages(CancellationToken cancellationToken)
    {
        // var user = GetUser();
        // var getUserImagesRequest = new ImageServiceGetUserImagesRequest(user.Id);
        // var getUserImagesResponse = await _imageService.GetUserImagesAsync(getUserImagesRequest, cancellationToken);
        // var response = new GetUserImagesResponse(getUserImagesResponse.Images.Select(x => new ImageModel(x.ImageInBytes, x.Format)));
        return Task.FromResult((ActionResult<GetImageResponse>)Ok());
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