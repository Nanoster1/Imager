using Throw;

namespace Imager.ImageStoreService.Core.Images.Models;

public class ImageObject(byte[] image, string format)
{
    public byte[] Image { get; set; } = image.ThrowIfNull().IfEmpty();
    public string Format { get; set; } = format.ThrowIfNull().IfEmpty();
}