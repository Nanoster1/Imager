using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using ImageStoreRoutes = Imager.ImageStoreService.Contracts.Routes.HttpRoutes;

using Refit;

namespace Imager.ImageResizerService.Core.Common.Services.Interfaces;

public interface ITempImageService
{
    [Get(ImageStoreRoutes.TempImage)]
    Task<GetTempImageResponse> GetImageAsync([Query] GetTempImageRequest request, CancellationToken cancellationToken);

    [Post(ImageStoreRoutes.TempImage)]
    Task<CreateTempImagesResponse> CreateImageAsync([Body] CreateTempImagesRequest request, CancellationToken cancellationToken);
}