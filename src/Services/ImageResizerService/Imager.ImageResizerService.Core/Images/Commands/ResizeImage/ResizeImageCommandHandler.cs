using Dumpify;

using ErrorOr;

using Imager.ImageResizerService.Core.Common.Services.Interfaces;

using Imager.ImageResizerService.Core.Images.Results;
using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.Models;

using MediatR;

using Microsoft.Extensions.Logging;

using SkiaSharp;

namespace Imager.ImageResizerService.Core.Images.Commands.ResizeImage;

public class ResizeImageCommandHandler(ITempImageService tempImageService, ILogger<ResizeImageCommandHandler> logger) : IRequestHandler<ResizeImageCommand, ErrorOr<ResizeImageResult>>
{
    private readonly ITempImageService _tempImageService = tempImageService;
    private readonly ILogger<ResizeImageCommandHandler> _logger = logger;

    public async Task<ErrorOr<ResizeImageResult>> Handle(ResizeImageCommand request, CancellationToken cancellationToken)
    {
        var getTempImageRequest = new GetTempImageRequest(request.ImageId, request.UserId);
        var getTempImageResponse = await _tempImageService.GetImageAsync(getTempImageRequest, cancellationToken);

        using var sourceBitmap = SKBitmap.Decode(getTempImageResponse.Image.ImageInBytes);
        using var scaledBitmap = sourceBitmap.Resize(new SKImageInfo(request.Width, request.Height), SKFilterQuality.High);
        using var scaledImage = SKImage.FromBitmap(scaledBitmap);
        using var data = scaledImage.Encode();

        var tempImageFileModel = new TempImageFileModel(data.ToArray(), getTempImageResponse.Image.Format);
        var createTempImagesRequest = new CreateTempImagesRequest(request.UserId, [tempImageFileModel]);
        var createTempImagesResponse = await _tempImageService.CreateImageAsync(createTempImagesRequest, cancellationToken);

        return new ResizeImageResult(request.UserId, createTempImagesResponse.ImageIds[0]);
    }
}