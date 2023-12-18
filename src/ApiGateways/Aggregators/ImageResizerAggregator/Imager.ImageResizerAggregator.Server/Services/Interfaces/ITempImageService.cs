using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using ImageStoreRoutes = Imager.ImageStoreService.Contracts.Routes.HttpRoutes;

using Refit;

namespace Imager.ImageResizerAggregator.Server.Services.Interfaces;

public interface ITempImageService
{
    [Get(ImageStoreRoutes.TempImageController)]
    Task<GetTempImageResponse> GetTempImageAsync([Query] GetTempImageRequest request, CancellationToken cancellationToken);

    [Post(ImageStoreRoutes.TempImageController)]
    Task<CreateTempImagesResponse> CreateTempImagesAsync([Body] CreateTempImagesRequest request, CancellationToken cancellationToken);
}