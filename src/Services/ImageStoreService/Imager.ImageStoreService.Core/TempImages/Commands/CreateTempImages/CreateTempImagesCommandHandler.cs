using ErrorOr;


using Imager.ImageStoreService.Core.Common.Services.Interfaces;
using Imager.ImageStoreService.Core.TempImages.Models;
using Imager.ImageStoreService.Core.TempImages.Results;
using Imager.ImageStoreService.Core.TempImages.Settings;

using MediatR;

using Microsoft.Extensions.Options;

using Throw;

namespace Imager.ImageStoreService.Core.TempImages.Commands.CreateTempImages;

public class CreateTempImagesCommandHandler(
    ITempImageObjectStore tempImageObjectRepository,
    ITempImageObjectKeyGenerator imageObjectKeyGenerator,
    IOptions<TempImagesSettings> tempImagesSettings) :
    IRequestHandler<CreateTempImagesCommand, ErrorOr<CreateTempImagesResult>>
{
    private readonly ITempImageObjectStore _tempImageObjectRepository = tempImageObjectRepository;
    private readonly ITempImageObjectKeyGenerator _imageObjectKeyGenerator = imageObjectKeyGenerator;
    private readonly TempImagesSettings _tempImagesSettings = tempImagesSettings.Value;

    public async Task<ErrorOr<CreateTempImagesResult>> Handle(CreateTempImagesCommand request, CancellationToken cancellationToken)
    {
        request.ThrowIfNull();

        var ids = new string[request.Images.Count];
        for (var i = 0; i < request.Images.Count; i++)
        {
            var image = request.Images[i];
            var key = await _imageObjectKeyGenerator.GenerateAsync(request.UserId, cancellationToken);
            var tempImageObject = new TempImageObject(image.ImageInBytes, image.Format);
            await _tempImageObjectRepository.CreateObjectAsync(key, tempImageObject, _tempImagesSettings.PresignTTL, cancellationToken);
            ids[i] = key.Value;
        }

        return new CreateTempImagesResult(ids);
    }
}