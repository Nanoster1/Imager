using Imager.Dapr.S3.Interfaces;
using Imager.ImageStoreService.Core.TempImages.Models;

namespace Imager.ImageStoreService.Core.Common.Services.Interfaces;

public interface ITempImageObjectStore : IObjectStore<TempImageObject>
{
}