using Imager.Dapr.S3.Models;

namespace Imager.Dapr.S3.Interfaces;

public interface IObjectStoreKeyGenerator
{
    public Task<ObjectStoreKey> GenerateAsync(string? prefix = null, CancellationToken cancellationToken = default);
}