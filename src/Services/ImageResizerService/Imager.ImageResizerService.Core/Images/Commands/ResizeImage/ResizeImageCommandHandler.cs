using ErrorOr;

using Imager.ImageResizerService.Core.Common.Services.Interfaces;

using Imager.ImageResizerService.Core.Images.Results;
using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.Models;

using MediatR;

using SkiaSharp;

namespace Imager.ImageResizerService.Core.Images.Commands.ResizeImage;

public class ResizeImageCommandHandler(ITempImageService tempImageService) : IRequestHandler<ResizeImageCommand, ErrorOr<ResizeImageResult>>
{
    private readonly ITempImageService _tempImageService = tempImageService;

    public async Task<ErrorOr<ResizeImageResult>> Handle(ResizeImageCommand request, CancellationToken cancellationToken)
    {
        var getTempImageRequest = new GetTempImageRequest(request.ImageId, request.UserId);
        var getTempImageResponse = await _tempImageService.GetImageAsync(getTempImageRequest, cancellationToken);

        var format = getTempImageResponse.Image.Format;
        if (!Enum.TryParse<SKEncodedImageFormat>(format, true, out var imageFormat))
        {
            return Error.Failure($"Invalid image format: {format ?? "null"}");
        }

        using var sourceBitmap = SKBitmap.Decode(getTempImageResponse.Image.ImageInBytes);
        var resizedInfo = new SKImageInfo(request.Width, request.Height);
        using SKBitmap resizedBitmap = sourceBitmap.Resize(resizedInfo, SKFilterQuality.High);
        if (resizedBitmap == null) return Error.Failure("Failed to resize image");
        using SKImage resizedImage = SKImage.FromBitmap(resizedBitmap);
        using SKData encodedData = resizedImage.Encode(imageFormat, 100);
        var resizedImageInBytes = encodedData.ToArray();

        var tempImageFileModel = new TempImageFileModel(resizedImageInBytes, format);
        var createTempImagesRequest = new CreateTempImagesRequest(request.UserId, [tempImageFileModel]);
        var createTempImagesResponse = await _tempImageService.CreateImageAsync(createTempImagesRequest, cancellationToken);

        return new ResizeImageResult(request.UserId, createTempImagesResponse.ImageIds[0]);
    }
}