using Imager.Dapr;
using Imager.Dapr.S3.Implementations;
using Imager.ImageStoreService.Core.Common.Repositories.Interfaces;
using Imager.ImageStoreService.Core.Common.Settings;

using Microsoft.Extensions.Options;

namespace Imager.ImageStoreService.Core.Common.Repositories.Implementations;

public class TempImageObjectRepository : DaprObjectStore, ITempImageObjectRepository
{
    public TempImageObjectRepository(
        HttpClient client,
        IOptions<DaprSettings> daprSettings) :
        base(daprSettings.Value.DaprPort, DaprComponentsNames.ImagerTempImages, client)
    {
    }
}