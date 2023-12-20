using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;

using Refit;

using ImageStoreRoutes = Imager.ImageStoreService.Contracts.Routes.HttpRoutes;

namespace Imager.ImageResizerAggregator.Server.Services.Interfaces;

public interface IImageService
{
    [Get(ImageStoreRoutes.ImageController)]
    Task<GetImageResponse> GetImageAsync([Query] GetImageRequest request, CancellationToken cancellationToken);

    [Post(ImageStoreRoutes.ImageController)]
    Task<CreateImageFromTempResponse> CreateImageFromTempAsync([Body] CreateImageFromTempRequest request, CancellationToken cancellationToken);
}