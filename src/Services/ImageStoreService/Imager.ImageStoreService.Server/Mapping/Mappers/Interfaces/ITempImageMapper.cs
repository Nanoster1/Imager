using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Core.TempImages.Commands.CreateTempImages;
using Imager.ImageStoreService.Core.TempImages.Queries.GetTempImage;
using Imager.ImageStoreService.Core.TempImages.Results;

using Mapster;

namespace Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces;

[Mapper]
public interface ITempImageMapper
{
    CreateTempImagesCommand Map(CreateTempImagesRequest request);
    CreateTempImagesResponse Map(CreateTempImagesResult result);
    GetTempImageQuery Map(GetTempImageRequest result);
    GetTempImageResponse Map(GetTempImageResult result);
}