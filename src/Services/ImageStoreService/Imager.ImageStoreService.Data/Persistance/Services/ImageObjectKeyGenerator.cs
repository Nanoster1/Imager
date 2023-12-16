using Imager.Dapr.S3.Implementations;
using Imager.ImageStoreService.Core.Common.Persistance.Services.Interfaces;

namespace Imager.ImageStoreService.Data.Persistance.Services;

public class TempImageObjectKeyGenerator : GuidObjectStoreKeyGenerator, ITempImageObjectKeyGenerator
{
}