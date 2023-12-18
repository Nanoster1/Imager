using Throw;

namespace Imager.ImageStoreService.Core.TempImages.Models;

public class TempImageObject(byte[] image, string format)
{
    public byte[] Image { get; set; } = image.ThrowIfNull().IfEmpty();
    public string Format { get; set; } = format.ThrowIfNull().IfEmpty();
}