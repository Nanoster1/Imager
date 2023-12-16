namespace Imager.ImageStoreService.Core.Domains.TempImages.Results;

public record GetTempImageResult(string ImageId, byte[] Image);