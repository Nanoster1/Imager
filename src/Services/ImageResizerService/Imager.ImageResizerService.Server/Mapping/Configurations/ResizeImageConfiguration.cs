using Imager.Dapr.Events;
using Imager.ImageResizerService.Core.Images.Commands.ResizeImage;

using Mapster;

namespace Imager.ImageResizerService.Server.Mapping.Configurations;

public class ResizeImageConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.RequireDestinationMemberSource = true;
        config.NewConfig<OnResizeImageEvent, ResizeImageCommand>();
        config.NewConfig<ResizeImageCommand, OnResizeImageEvent>();
    }
}