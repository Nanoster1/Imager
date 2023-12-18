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
    public CreateTempImagesCommand Map(CreateTempImagesRequest request);
    public CreateTempImagesResponse Map(CreateTempImagesResult result);
    public GetTempImageQuery Map(GetTempImageRequest result);
    public GetTempImageResponse Map(GetTempImageResult result);
}