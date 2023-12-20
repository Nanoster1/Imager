using Imager.ImageStoreService.Contracts.Models;

namespace Imager.ImageStoreService.Contracts.HttpResponses;

public record GetTempImageResponse(string ImageId, TempImageFileModel Image);