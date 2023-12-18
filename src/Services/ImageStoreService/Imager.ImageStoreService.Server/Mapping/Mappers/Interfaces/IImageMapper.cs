using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Core.Images.Commands.CreateImageFromTemp;
using Imager.ImageStoreService.Core.Images.Queries.GetImage;
using Imager.ImageStoreService.Core.Images.Results;

using Mapster;

namespace Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces;

[Mapper]
public interface IImageMapper
{
    CreateImageFromTempCommand Map(CreateImageFromTempRequest request);
    CreateImageFromTempResponse Map(CreateImageFromTempResult result);
    GetImageQuery Map(GetImageRequest request);
    GetImageResponse Map(GetImageResult result);
}