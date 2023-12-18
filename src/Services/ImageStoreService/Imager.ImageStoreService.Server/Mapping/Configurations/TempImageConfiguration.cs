using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Core.TempImages.Commands.CreateTempImages;
using Imager.ImageStoreService.Core.TempImages.Queries.GetTempImage;
using Imager.ImageStoreService.Core.TempImages.Results;

using Mapster;

namespace Imager.ImageStoreService.Server.Mapping.Configurations;

public class TempImageConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.RequireDestinationMemberSource = true;
        config.NewConfig<CreateTempImagesRequest, CreateTempImagesCommand>();
        config.NewConfig<CreateTempImagesResult, CreateTempImagesResponse>();
        config.NewConfig<GetTempImageRequest, GetTempImageQuery>();
        config.NewConfig<GetTempImageResult, GetTempImageResponse>();
    }
}