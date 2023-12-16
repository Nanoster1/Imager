using Throw;

namespace Imager.Dapr.S3.Models;

public class ObjectData<TValue>(ObjectStoreKey key, TValue value)
{
    public ObjectStoreKey Key { get; private set; } = key.ThrowIfNull();
    public TValue Value { get; private set; } = value;
}