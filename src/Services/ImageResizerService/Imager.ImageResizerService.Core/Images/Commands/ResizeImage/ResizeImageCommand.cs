using ErrorOr;


using Imager.ImageResizerService.Core.Images.Results;

using MediatR;

namespace Imager.ImageResizerService.Core.Images.Commands.ResizeImage;

public record ResizeImageCommand(
    string ImageId,
    string UserId,
    int Width,
    int Height) :
    IRequest<ErrorOr<ResizeImageResult>>;