using Imager.ImageResizerAggregator.Contracts.Models;

namespace Imager.ImageResizerAggregator.Contracts.Requests;

public record StartResizeImagesRequest(int Height, int Width, IEnumerable<ImageModel> ImageModels);