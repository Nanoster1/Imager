using Imager.ImageStoreService.Core.Images.Models;

namespace Imager.ImageStoreService.Core.Images.Results;

public record GetImageResult(string ImageId, ImageFileModel Image);