namespace Imager.ImageResizerAggregator.Contracts.Requests;

public record ImageModel(byte[] ImageInBytes, string Format);
public record StartResizeImagesRequest(int Height, int Width, IEnumerable<ImageModel> ImageModels);