using ErrorOr;

using Imager.ImageStoreService.Core.Images.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.Images.Queries.GetImage;

public record GetImageQuery(string UserId, string ImageId) : IRequest<ErrorOr<GetImageResult>>;