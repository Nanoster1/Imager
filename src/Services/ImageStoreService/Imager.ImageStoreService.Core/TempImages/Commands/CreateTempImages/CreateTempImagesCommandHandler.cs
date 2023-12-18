using ErrorOr;


using Imager.ImageStoreService.Core.Common.Repositories.Interfaces;
using Imager.ImageStoreService.Core.Common.Services.Interfaces;
using Imager.ImageStoreService.Core.TempImages.Models;
using Imager.ImageStoreService.Core.TempImages.Results;
using Imager.ImageStoreService.Core.TempImages.Settings;

using MediatR;

using Microsoft.Extensions.Options;

using Throw;

namespace Imager.ImageStoreService.Core.TempImages.Commands.CreateTempImages;

public class CreateTempImagesCommandHandler(
    ITempImageObjectRepository tempImageObjectRepository,
    ITempImageObjectKeyGenerator imageObjectKeyGenerator,
    IOptions<TempImagesSettings> tempImagesSettings) :
    IRequestHandler<CreateTempImagesCommand, ErrorOr<CreateTempImagesResult>>
{
    private readonly ITempImageObjectRepository _tempImageObjectRepository = tempImageObjectRepository;
    private readonly ITempImageObjectKeyGenerator _imageObjectKeyGenerator = imageObjectKeyGenerator;
    private readonly TempImagesSettings _tempImagesSettings = tempImagesSettings.Value;

    public async Task<ErrorOr<CreateTempImagesResult>> Handle(CreateTempImagesCommand request, CancellationToken cancellationToken)
    {
        request.ThrowIfNull();

        var ids = new List<string>(request.Images.Count);
        foreach (var image in request.Images)
        {
            var key = await _imageObjectKeyGenerator.GenerateAsync(request.UserId, cancellationToken);
            var tempImageObject = new TempImageObject(image);
            await _tempImageObjectRepository.CreateObjectAsync(key, tempImageObject, _tempImagesSettings.PresignTTL, cancellationToken);
            ids.Add(key.Value);
        }

        return new CreateTempImagesResult(ids);
    }
}