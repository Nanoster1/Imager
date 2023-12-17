using ErrorOr;

using Imager.ImageStoreService.Core.TempImages.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.TempImages.Commands.CreateTempImages;

public record CreateTempImagesCommand(string UserId, List<byte[]> Images) : IRequest<ErrorOr<CreateTempImagesResult>>;