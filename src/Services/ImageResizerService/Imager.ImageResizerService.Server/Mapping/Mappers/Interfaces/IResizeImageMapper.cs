using Imager.Dapr.Events;
using Imager.ImageResizerService.Core.Images.Commands.ResizeImage;
using Imager.ImageResizerService.Core.Images.Results;

using Mapster;

namespace Imager.ImageResizerService.Server.Mapping.Mappers.Interfaces;

[Mapper]
public interface IResizeImageMapper
{
    public ResizeImageCommand Map(OnResizeImageEvent @event);
    public OnImageResizedEvent Map(ResizeImageResult command);
}