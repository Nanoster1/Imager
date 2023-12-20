using ErrorOr;

using Imager.ImageStoreService.Core.Common.Services.Interfaces;
using Imager.ImageStoreService.Core.Images.Models;

using Imager.ImageStoreService.Core.Images.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.Images.Commands.CreateImageFromTemp;

public class CreateImageFromTempCommandHandler(
    IImageObjectStore imageObjectStore,
    ITempImageObjectStore tempImageObjectStore,
    ITempImageObjectKeyGenerator objectStoreKeyGenerator) :
    IRequestHandler<CreateImageFromTempCommand, ErrorOr<CreateImageFromTempResult>>
{
    private readonly IImageObjectStore _imageObjectStore = imageObjectStore;
    private readonly ITempImageObjectStore _tempImageObjectStore = tempImageObjectStore;
    private readonly ITempImageObjectKeyGenerator _objectStoreKeyGenerator = objectStoreKeyGenerator;

    public async Task<ErrorOr<CreateImageFromTempResult>> Handle(CreateImageFromTempCommand request, CancellationToken cancellationToken)
    {
        var tempImage = await _tempImageObjectStore.GetObjectAsync(new(request.UserId, request.TempImageId), cancellationToken);
        if (tempImage is null) return Error.NotFound("Temp image not found");
        var newKey = await _objectStoreKeyGenerator.GenerateAsync(request.UserId, cancellationToken);
        var image = new ImageObject(tempImage.Value.Image, tempImage.Value.Format);
        try
        {
            await _imageObjectStore.CreateObjectAsync(newKey, image, cancellationToken: cancellationToken);
        }
        catch (Exception)
        {
            return Error.Failure("Failed to create image");
        }
        return new CreateImageFromTempResult(request.UserId, newKey.Value);
    }
}