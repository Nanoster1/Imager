using ErrorOr;

using Imager.ImageStoreService.Core.Common.Services.Interfaces;
using Imager.ImageStoreService.Core.Images.Models;

using Imager.ImageStoreService.Core.Images.Results;

using MediatR;

namespace Imager.ImageStoreService.Core.Images.Queries.GetUserImages;

public class GetUserImagesQueryHandler(IImageObjectStore imageObjectStore) : IRequestHandler<GetUserImagesQuery, ErrorOr<GetUserImagesResult>>
{
    private readonly IImageObjectStore _imageObjectStore = imageObjectStore;

    public async Task<ErrorOr<GetUserImagesResult>> Handle(GetUserImagesQuery request, CancellationToken cancellationToken)
    {
        var listObjectsResult = await _imageObjectStore.ListObjectsAsync(cancellationToken: cancellationToken);
        var imageFileModels = new ImageFileModel[listObjectsResult.Contents.Count];
        for (var i = 0; i < listObjectsResult.Contents.Count; i++)
        {
            var image = listObjectsResult.Contents[i];
            imageFileModels[i] = new ImageFileModel(image.Image, image.Format);
        }
        return new GetUserImagesResult(imageFileModels);
    }
}