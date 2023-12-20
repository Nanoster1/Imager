using ErrorOr;

using Imager.ImageStoreService.Core.Common.Services.Interfaces;
using Imager.ImageStoreService.Core.Images.Models;

using Imager.ImageStoreService.Core.Images.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.Images.Queries.GetImage;

public class GetImageQueryHandler(
    IImageObjectStore imageObjectStore) :
    IRequestHandler<GetImageQuery, ErrorOr<GetImageResult>>
{
    private readonly IImageObjectStore _imageObjectStore = imageObjectStore;

    public async Task<ErrorOr<GetImageResult>> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var image = await _imageObjectStore.GetObjectAsync(new(request.UserId, request.ImageId), cancellationToken);
        if (image is null) return Error.NotFound("Image not found");
        var imageFileModel = new ImageFileModel(image.Value.Image, image.Value.Format);
        return new GetImageResult(image.Key.Value, imageFileModel);
    }
}