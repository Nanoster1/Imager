using Imager.ImageResizerService.Contracts.Events;
using Imager.ImageResizerService.Core.Images.Commands.ResizeImage;
using Imager.ImageResizerService.Server.Mapping.Mappers.Interfaces;

namespace Imager.ImageResizerService.Server.Mapping
{
    public partial class ResizeImageMapper : IResizeImageMapper
    {
        public ResizeImageCommand Map(ResizeImageEvent p1)
        {
            return p1 == null ? null : new ResizeImageCommand(p1.ImageId, p1.UserId, p1.Width, p1.Height, p1.ImageFormat);
        }
    }
}