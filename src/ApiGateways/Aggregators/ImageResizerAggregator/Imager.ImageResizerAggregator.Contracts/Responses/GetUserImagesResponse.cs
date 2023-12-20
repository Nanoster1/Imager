using Imager.ImageResizerAggregator.Contracts.Models;

namespace Imager.ImageResizerAggregator.Contracts.Responses;

public record GetUserImagesResponse(IEnumerable<string> Ids, IEnumerable<ImageModel> Images);