using Imager.ImageResizerService.Contracts.Events;
using Imager.ImageResizerService.Core.Images.Commands.ResizeImage;

using Mapster;

namespace Imager.ImageResizerService.Server.Mapping.Mappers.Interfaces;

[Mapper]
public interface IResizeImageMapper
{
    public ResizeImageCommand Map(ResizeImageEvent @event);
}