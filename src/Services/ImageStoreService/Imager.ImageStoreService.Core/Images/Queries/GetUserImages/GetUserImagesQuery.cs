using ErrorOr;

using Imager.ImageStoreService.Core.Images.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.Images.Queries.GetUserImages;

public record GetUserImagesQuery(string UserId) : IRequest<ErrorOr<GetUserImagesResult>>;