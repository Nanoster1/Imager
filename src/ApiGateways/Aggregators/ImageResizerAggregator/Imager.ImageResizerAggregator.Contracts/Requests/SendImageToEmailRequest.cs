namespace Imager.ImageResizerAggregator.Contracts.Requests;

public record SendImageToEmailRequest(string ImageId, string UserEmail);