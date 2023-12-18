using Imager.Dapr;
using Imager.Dapr.S3.Implementations;
using Imager.ImageStoreService.Core.Common.Services.Interfaces;
using Imager.ImageStoreService.Core.Common.Settings;
using Imager.ImageStoreService.Core.TempImages.Models;

using Microsoft.Extensions.Options;

namespace Imager.ImageStoreService.Core.Common.Services.Implementations;

public class TempImageObjectStore : DaprObjectStore<TempImageObject>, ITempImageObjectStore
{
    public TempImageObjectStore(
        HttpClient client,
        IOptions<DaprSettings> daprSettings) :
        base(daprSettings.Value.DaprPort, DaprComponentsNames.ImagerTempImages, client)
    {
    }
}