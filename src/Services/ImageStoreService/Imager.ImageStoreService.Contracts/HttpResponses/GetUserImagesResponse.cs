using Imager.ImageStoreService.Contracts.Models;

namespace Imager.ImageStoreService.Contracts.HttpResponses;

public record GetUserImagesResponse(ImageFileModel[] Images);