using Imager.Dapr.S3.Models;
using Imager.Dapr.S3.Responses;

namespace Imager.Dapr.S3.Interfaces;

public interface IObjectStore<TObject> where TObject : notnull
{
    Task<CreateObjectResponse> CreateObjectAsync(
        ObjectStoreKey key,
        TObject data,
        string? presignTTL = null,
        CancellationToken cancellationToken = default);
    Task<bool> IsObjectExistsAsync(
        ObjectStoreKey key,
        CancellationToken cancellationToken = default);
    Task<PresignObjectResponse> PresignObjectAsync(
        ObjectStoreKey key,
        string presignTTL,
        CancellationToken cancellationToken = default);
    Task<ObjectData<TObject>?> GetObjectAsync(
        ObjectStoreKey key,
        CancellationToken cancellationToken = default);
    Task DeleteObjectAsync(
        ObjectStoreKey key,
        CancellationToken cancellationToken = default);
    Task<ListObjectsResponse> ListObjectsAsync(
        int maxResults = 1000,
        string? prefix = null,
        string? delimiter = null,
        string? marker = null,
        CancellationToken cancellationToken = default);
}