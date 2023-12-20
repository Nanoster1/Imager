using Imager.ImageStoreService.Core.Images.Models;

namespace Imager.ImageStoreService.Core.Images.Results;

public record GetUserImagesResult(ImageFileModel[] Images);