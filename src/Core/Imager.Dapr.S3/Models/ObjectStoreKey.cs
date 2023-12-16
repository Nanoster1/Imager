using Throw;

namespace Imager.Dapr.S3.Models;

public class ObjectStoreKey
{
    public const char Delimiter = '_';

    public ObjectStoreKey(string key)
    {
        key.ThrowIfNull();
        var parts = key.Split(Delimiter);
        if (parts.Length != 2) throw new ArgumentException("Invalid key", nameof(key));
        Prefix = parts[0];
        Value = parts[1];
    }

    public ObjectStoreKey(string prefix, string value)
    {
        Prefix = prefix.ThrowIfNull();
        Value = value.ThrowIfNull();
    }

    public string Prefix { get; set; }
    public string Value { get; set; }

    public override string ToString()
    {
        return $"{Prefix}{Delimiter}{Value}";
    }

    public static implicit operator string(ObjectStoreKey key)
    {
        return key.ToString();
    }
}