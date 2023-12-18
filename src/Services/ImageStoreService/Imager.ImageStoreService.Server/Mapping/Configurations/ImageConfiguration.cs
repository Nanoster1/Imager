using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Core.Images.Commands.CreateImageFromTemp;
using Imager.ImageStoreService.Core.Images.Queries.GetImage;
using Imager.ImageStoreService.Core.Images.Results;

using Mapster;

namespace Imager.ImageStoreService.Server.Mapping.Configurations;

public class ImageConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.RequireDestinationMemberSource = true;
        config.NewConfig<CreateImageFromTempRequest, CreateImageFromTempCommand>();
        config.NewConfig<CreateImageFromTempResult, CreateImageFromTempResponse>();
        config.NewConfig<GetImageRequest, GetImageQuery>();
        config.NewConfig<GetImageResult, GetImageResponse>();
    }
}