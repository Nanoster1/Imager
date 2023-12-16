using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Core.Domains.TempImages.Commands.CreateTempImages;
using Imager.ImageStoreService.Core.Domains.TempImages.Queries.GetTempImage;
using Imager.ImageStoreService.Core.Domains.TempImages.Results;
using Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces.Common;

using Mapster;

namespace Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces;

[Mapper]
public interface ITempImageMapper : IMapper
{
    public CreateTempImagesCommand Map(CreateTempImagesRequest request);
    public CreateTempImageResponse Map(CreateTempImageResult result);
    public GetTempImageQuery Map(GetTempImageRequest result);
    public GetTempImageResponse Map(GetTempImageResult result);
}