using ErrorOr;

using Imager.ImageStoreService.Core.Domains.TempImages.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.Domains.TempImages.Queries.GetTempImage;

public record GetTempImageQuery(string ImageId, string UserId) : IRequest<ErrorOr<GetTempImageResult>>;