using Imager.Dapr;
using Imager.Dapr.S3.Implementations;
using Imager.ImageStoreService.Core.Common.Services.Interfaces;
using Imager.ImageStoreService.Core.Common.Settings;
using Imager.ImageStoreService.Core.Images.Models;

using Microsoft.Extensions.Options;

namespace Imager.ImageStoreService.Core.Common.Services.Implementations;

public class ImageObjectStore : DaprObjectStore<ImageObject>, IImageObjectStore
{
    public ImageObjectStore(
        HttpClient client,
        IOptions<DaprSettings> daprSettings) :
        base(daprSettings.Value.DaprPort, DaprComponentsNames.ImagerImages, client)
    {
    }
}