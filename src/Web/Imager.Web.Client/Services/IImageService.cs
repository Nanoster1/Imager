using Imager.ImageResizerAggregator.Contracts.Requests;
using Imager.ImageResizerAggregator.Contracts.Responses;
using Imager.ImageResizerAggregator.Contracts.Routes;

using Refit;

namespace Imager.Web.Client.Services;

public interface IImageService
{
    [Get(HttpRoutes.ImageController)]
    Task<GetImageResponse> GetImageAsync([Query] GetImageRequest request);

    [Post($"{HttpRoutes.ImageController}/email")]
    Task SendImageToEmailAsync([Body] SendImageToEmailRequest request);
    [Get($"{HttpRoutes.ImageController}/all")]
    Task<GetUserImagesResponse> GetUserImagesAsync();
}