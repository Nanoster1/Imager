using Imager.ImageStoreService.Contracts.Models;

namespace Imager.ImageStoreService.Contracts.HttpRequests;

public record CreateTempImagesRequest(string UserId, List<TempImageFileModel> Images);