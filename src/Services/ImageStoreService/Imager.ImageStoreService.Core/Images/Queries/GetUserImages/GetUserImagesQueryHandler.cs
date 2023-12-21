using Dumpify;

using ErrorOr;

using Imager.Dapr.S3.Models;

using Imager.ImageStoreService.Core.Common.Services.Interfaces;

using Imager.ImageStoreService.Core.Images.Results;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Imager.ImageStoreService.Core.Images.Queries.GetUserImages;

public class GetUserImagesQueryHandler(IImageObjectStore imageObjectStore, ILogger<GetUserImagesQueryHandler> logger) : IRequestHandler<GetUserImagesQuery, ErrorOr<GetUserImagesResult>>
{
    private readonly IImageObjectStore _imageObjectStore = imageObjectStore;
    private readonly ILogger<GetUserImagesQueryHandler> _logger = logger;

    public async Task<ErrorOr<GetUserImagesResult>> Handle(GetUserImagesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{r}", request.DumpText());
        var listObjectsResult = await _imageObjectStore.ListObjectsAsync(prefix: request.UserId, cancellationToken: cancellationToken);
        var contentsCount = listObjectsResult.Contents?.Count ?? 0;
        var ids = new string[contentsCount];
        for (var i = 0; i < contentsCount; i++)
        {
            var image = listObjectsResult.Contents![i];
            ids[i] = new ObjectStoreKey(image.Key).Value;
        }
        return new GetUserImagesResult(ids);
    }
}