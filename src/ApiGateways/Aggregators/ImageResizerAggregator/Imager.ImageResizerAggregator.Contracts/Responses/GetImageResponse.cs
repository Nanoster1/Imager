using Imager.ImageResizerAggregator.Contracts.Models;

namespace Imager.ImageResizerAggregator.Contracts.Responses;

public record GetImageResponse(string ImageId, ImageModel Image);