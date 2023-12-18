namespace Imager.ImageStoreService.Contracts.HttpRequests;

public record CreateImageFromTempRequest(string UserId, string TempImageId);