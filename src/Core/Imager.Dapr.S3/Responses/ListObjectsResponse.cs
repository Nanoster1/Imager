namespace Imager.Dapr.S3.Responses;

public record ListObjectsResponse(
    string CommonPrefixes,
    List<object> Contents,
    string Delimiter,
    string EncodingType,
    bool IsTruncated,
    string Marker,
    int MaxKeys,
    string Name,
    string NextMarker,
    string Prefix);