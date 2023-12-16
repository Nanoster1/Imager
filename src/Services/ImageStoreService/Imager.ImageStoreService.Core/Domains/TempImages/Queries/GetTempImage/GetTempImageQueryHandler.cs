using ErrorOr;

using Imager.Dapr.S3.Models;

using Imager.ImageStoreService.Core.Common.Persistance.Repositories;

using Imager.ImageStoreService.Core.Domains.TempImages.Models;
using Imager.ImageStoreService.Core.Domains.TempImages.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.Domains.TempImages.Queries.GetTempImage;

public class GetTempImageQueryHandler(ITempImageObjectRepository tempImageObjectRepository) :
    IRequestHandler<GetTempImageQuery, ErrorOr<GetTempImageResult>>
{
    private readonly ITempImageObjectRepository _tempImageObjectRepository = tempImageObjectRepository;

    public async Task<ErrorOr<GetTempImageResult>> Handle(GetTempImageQuery request, CancellationToken cancellationToken)
    {
        var key = new ObjectStoreKey(request.UserId, request.ImageId);
        var image = await _tempImageObjectRepository.GetObjectAsync<TempImageObject>(key, cancellationToken);
        return image is null ? Error.NotFound() : new GetTempImageResult(image.Key.Value, image.Value.Image);
    }
}