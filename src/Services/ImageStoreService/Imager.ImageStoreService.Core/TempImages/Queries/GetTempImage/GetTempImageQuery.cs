using ErrorOr;

using Imager.ImageStoreService.Core.TempImages.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.TempImages.Queries.GetTempImage;

public record GetTempImageQuery(string ImageId, string UserId) : IRequest<ErrorOr<GetTempImageResult>>;