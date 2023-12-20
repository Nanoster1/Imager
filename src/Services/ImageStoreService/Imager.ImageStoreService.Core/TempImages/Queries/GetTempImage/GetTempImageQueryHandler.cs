using ErrorOr;


using Imager.Dapr.S3.Models;
using Imager.ImageStoreService.Core.Common.Services.Interfaces;
using Imager.ImageStoreService.Core.TempImages.Models;
using Imager.ImageStoreService.Core.TempImages.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.TempImages.Queries.GetTempImage;

public class GetTempImageQueryHandler(ITempImageObjectStore tempImageObjectRepository) :
    IRequestHandler<GetTempImageQuery, ErrorOr<GetTempImageResult>>
{
    private readonly ITempImageObjectStore _tempImageObjectRepository = tempImageObjectRepository;

    public async Task<ErrorOr<GetTempImageResult>> Handle(GetTempImageQuery request, CancellationToken cancellationToken)
    {
        var key = new ObjectStoreKey(request.UserId, request.ImageId);
        var image = await _tempImageObjectRepository.GetObjectAsync(key, cancellationToken);
        if (image is null) return Error.NotFound();
        var imageFileModel = new TempImageFileModel(image.Value.Image, image.Value.Format);
        return new GetTempImageResult(image.Key.Value, imageFileModel);
    }
}