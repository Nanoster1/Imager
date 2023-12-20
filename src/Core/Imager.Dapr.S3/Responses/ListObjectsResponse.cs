namespace Imager.Dapr.S3.Responses;

public record ListObjectsResponse<TObject>(
    string CommonPrefixes,
    List<TObject> Contents,
    string Delimiter,
    string EncodingType,
    bool IsTruncated,
    string Marker,
    int MaxKeys,
    string Name,
    string NextMarker,
    string Prefix);