namespace Imager.Dapr.S3.Responses;

public record CreateObjectResponse(Uri Location, string VersionId, Uri PresignURL);