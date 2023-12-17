using Imager.Dapr.S3.Implementations;
using Imager.ImageStoreService.Core.Common.Services.Interfaces;

namespace Imager.ImageStoreService.Core.Common.Services.Implementations;

public class TempImageObjectKeyGenerator : GuidObjectStoreKeyGenerator, ITempImageObjectKeyGenerator
{
}