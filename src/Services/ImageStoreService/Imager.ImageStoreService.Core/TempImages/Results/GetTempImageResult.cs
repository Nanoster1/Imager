using Imager.ImageStoreService.Core.TempImages.Models;

namespace Imager.ImageStoreService.Core.TempImages.Results;

public record GetTempImageResult(string ImageId, TempImageFileModel Image);