using Imager.Dapr.S3.Models;
using Imager.Dapr.S3.Responses;

namespace Imager.Dapr.S3.Interfaces;

public interface IObjectStore
{
    Task<CreateObjectResponse> CreateObjectAsync<TObject>(
        ObjectStoreKey key,
        TObject data,
        string? presignTTL = null,
        CancellationToken cancellationToken = default)
        where TObject : notnull;
    Task<bool> IsObjectExistsAsync(
        ObjectStoreKey key,
        CancellationToken cancellationToken = default);
    Task<PresignObjectResponse> PresignObjectAsync(
        ObjectStoreKey key,
        string presignTTL,
        CancellationToken cancellationToken = default);
    Task<ObjectData<TObject>?> GetObjectAsync<TObject>(
        ObjectStoreKey key,
        CancellationToken cancellationToken = default)
        where TObject : notnull;
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