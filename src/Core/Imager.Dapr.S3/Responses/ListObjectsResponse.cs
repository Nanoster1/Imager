namespace Imager.Dapr.S3.Responses;

public record ListObjectsResponse<TObject>(
    string CommonPrefixes,
    List<Contents<TObject>>? Contents,
    string Delimiter,
    string EncodingType,
    bool IsTruncated,
    string Marker,
    int MaxKeys,
    string Name,
    string NextMarker,
    string Prefix);

public record Contents<TObject>(
    string ETag,
    string Key,
    string LastModified,
    TObject Object,
    int Size,
    string StorageClass
);