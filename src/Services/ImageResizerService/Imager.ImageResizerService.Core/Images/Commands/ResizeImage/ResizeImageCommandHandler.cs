using ErrorOr;

using Imager.ImageResizerService.Core.Common.Services.Interfaces;

using Imager.ImageResizerService.Core.Images.Results;
using Imager.ImageStoreService.Contracts.HttpRequests;

using MediatR;

using SkiaSharp;

namespace Imager.ImageResizerService.Core.Images.Commands.ResizeImage;

public class ResizeImageCommandHandler(ITempImageService tempImageService) : IRequestHandler<ResizeImageCommand, ErrorOr<ResizeImageResult>>
{
    private readonly ITempImageService _tempImageService = tempImageService;

    public async Task<ErrorOr<ResizeImageResult>> Handle(ResizeImageCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<SKEncodedImageFormat>(request.ImageFormat, true, out var imageFormat))
        {
            return Error.Failure($"Invalid image format: {request.ImageFormat ?? "null"}");
        }

        var getTempImageRequest = new GetTempImageRequest(request.ImageId, request.UserId);
        var getTempImageResponse = await _tempImageService.GetImageAsync(getTempImageRequest, cancellationToken);

        using var sourceBitmap = SKBitmap.Decode(getTempImageResponse.Image);
        var resizedInfo = new SKImageInfo(request.Width, request.Height);
        using SKBitmap resizedBitmap = sourceBitmap.Resize(resizedInfo, SKFilterQuality.High);
        if (resizedBitmap == null) return Error.Failure("Failed to resize image");
        using SKImage resizedImage = SKImage.FromBitmap(resizedBitmap);
        using SKData encodedData = resizedImage.Encode(imageFormat, 100);

        var result = new ResizeImageResult(encodedData.ToArray());
        return result;
    }
}