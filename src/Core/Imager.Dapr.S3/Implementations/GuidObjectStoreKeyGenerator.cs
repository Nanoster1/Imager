using Imager.Dapr.S3.Interfaces;
using Imager.Dapr.S3.Models;

namespace Imager.Dapr.S3.Implementations;

public class GuidObjectStoreKeyGenerator : IObjectStoreKeyGenerator
{
    public Task<ObjectStoreKey> GenerateAsync(string? prefix = null, CancellationToken cancellationToken = default)
    {
        prefix = prefix is null ? string.Empty : $"{prefix}{ObjectStoreKey.Delimiter}";
        bool keyIsUnique;
        ObjectStoreKey key;
        do
        {
            key = new ObjectStoreKey($"{prefix}{Guid.NewGuid()}");
            keyIsUnique = true;
        }
        while (!keyIsUnique);

        return Task.FromResult(key);
    }
}