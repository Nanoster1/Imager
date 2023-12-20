using ErrorOr;

using Imager.ImageStoreService.Core.TempImages.Models;
using Imager.ImageStoreService.Core.TempImages.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.TempImages.Commands.CreateTempImages;

public record CreateTempImagesCommand(string UserId, List<TempImageFileModel> Images) : IRequest<ErrorOr<CreateTempImagesResult>>;