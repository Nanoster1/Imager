using Imager.Dapr;
using Imager.Dapr.S3.Implementations;
using Imager.ImageStoreService.Core.Common.Persistance.Repositories;
using Imager.ImageStoreService.Data.Persistance.Settings;

using Microsoft.Extensions.Options;

namespace Imager.ImageStoreService.Data.Persistance.Repositories;

public class TempImageObjectRepository : DaprObjectStore, ITempImageObjectRepository
{
    public TempImageObjectRepository(
        HttpClient client,
        IOptions<DaprSettings> daprSettings) :
        base(daprSettings.Value.DaprPort, DaprComponentsNames.ImagerTempImages, client)
    {
    }
}