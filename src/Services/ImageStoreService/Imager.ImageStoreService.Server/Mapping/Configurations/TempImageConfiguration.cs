using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Core.Domains.TempImages.Commands.CreateTempImages;
using Imager.ImageStoreService.Core.Domains.TempImages.Results;

using Mapster;

namespace Imager.ImageStoreService.Server.Mapping.Configurations;

public class TempImageConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.RequireDestinationMemberSource = true;
        config.NewConfig<CreateTempImagesRequest, CreateTempImagesCommand>();
        config.NewConfig<CreateTempImageResult, CreateTempImageResponse>();
    }
}