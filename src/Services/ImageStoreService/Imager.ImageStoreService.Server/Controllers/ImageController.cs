using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Contracts.Routes;
using Imager.ImageStoreService.Server.Controllers.Common;
using Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageStoreService.Server.Controllers;

[Route(HttpRoutes.ImageController)]
public class ImageController(ISender sender, IImageMapper imageMapper) : ApiController
{
    private readonly ISender _sender = sender;
    private readonly IImageMapper _mapper = imageMapper;

    [HttpPost]
    public async Task<ActionResult<CreateImageFromTempResponse>> CreateImageFromTemp(
        [FromBody] CreateImageFromTempRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map(request);
        var result = await _sender.Send(command, cancellationToken);
        return result.IsError
            ? BadRequest(result.Errors)
            : Created(string.Empty, _mapper.Map(result.Value));
    }

    [HttpGet]
    public async Task<ActionResult<GetImageResponse>> GetImage(
        [FromQuery] GetImageRequest request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map(request);
        var result = await _sender.Send(query, cancellationToken);
        return result.IsError
            ? BadRequest(result.Errors)
            : Ok(_mapper.Map(result.Value));
    }

    [HttpGet("all")]
    public async Task<ActionResult<GetUserImagesResponse>> GetUserImages(
        GetUserImagesRequest request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map(request);
        var result = await _sender.Send(query, cancellationToken);
        return result.IsError
            ? BadRequest(result.Errors)
            : Ok(_mapper.Map(result.Value));
    }
}