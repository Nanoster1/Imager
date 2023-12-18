namespace Imager.ImageStoreService.Core.TempImages.Results;

public record GetTempImageResult(string ImageId, byte[] Image);