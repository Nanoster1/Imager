using Throw;

namespace Imager.ImageStoreService.Core.Domains.TempImages.Models;

public class TempImageObject(byte[] image)
{
    public byte[] Image { get; set; } = image.ThrowIfNull().IfEmpty();
}