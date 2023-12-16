using ErrorOr;


using Imager.ImageStoreService.Core.Domains.TempImages.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.Domains.TempImages.Commands.CreateTempImages;

public record CreateTempImagesCommand(string UserId, List<byte[]> Images) : IRequest<ErrorOr<CreateTempImageResult>>;