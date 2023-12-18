using ErrorOr;

using Imager.ImageStoreService.Core.Images.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.Images.Commands.CreateImageFromTemp;

public record CreateImageFromTempCommand(string UserId, string TempImageId) : IRequest<ErrorOr<CreateImageFromTempResult>>;