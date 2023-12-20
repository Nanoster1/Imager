using Imager.ImageStoreService.Contracts.Models;

namespace Imager.ImageStoreService.Contracts.HttpResponses;

public record GetImageResponse(string ImageId, ImageFileModel Image);