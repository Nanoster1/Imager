using Imager.ImageResizerAggregator.Contracts.Requests;
using Imager.ImageResizerAggregator.Contracts.Responses;
using Imager.ImageResizerAggregator.Contracts.Routes;

using Refit;

namespace Imager.Web.Client.Services;

public interface IResizeService
{
    [Post(HttpRoutes.ImageResizeController)]
    Task<StartResizeImagesResponse> StartResizeImagesResponse([Body] StartResizeImagesRequest request);
}