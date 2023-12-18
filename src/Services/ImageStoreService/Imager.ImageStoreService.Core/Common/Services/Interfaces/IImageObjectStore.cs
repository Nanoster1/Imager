using Imager.Dapr.S3.Interfaces;
using Imager.ImageStoreService.Core.Images.Models;

namespace Imager.ImageStoreService.Core.Common.Services.Interfaces;

public interface IImageObjectStore : IObjectStore<ImageObject>
{

}