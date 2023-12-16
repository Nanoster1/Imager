namespace Imager.ImageStoreService.Contracts.HttpResponses;

public record GetTempImageResponse(string ImageId, byte[] Image);