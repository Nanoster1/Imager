using Imager.Dapr.Events;
using Imager.ImageResizerService.Core.Images.Commands.ResizeImage;
using Imager.ImageResizerService.Core.Images.Results;
using Imager.ImageResizerService.Server.Mapping.Mappers.Interfaces;

namespace Imager.ImageResizerService.Server.Mapping
{
    public partial class ResizeImageMapper : IResizeImageMapper
    {
        public ResizeImageCommand Map(OnResizeImageEvent p1)
        {
            return p1 == null ? null : new ResizeImageCommand(p1.ImageId, p1.UserId, p1.Width, p1.Height);
        }
        public OnImageResizedEvent Map(ResizeImageResult p2)
        {
            return p2 == null ? null : new OnImageResizedEvent(p2.UserId, p2.ResizedImageId);
        }
    }
}