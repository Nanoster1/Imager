namespace Imager.ImageStoreService.Contracts.HttpRequests;

public record CreateTempImagesRequest(string UserId, List<byte[]> Images);