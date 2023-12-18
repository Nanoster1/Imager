using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Contracts.Routes;
using Imager.ImageStoreService.Server.Controllers.Common;
using Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageStoreService.Server.Controllers;

[Route(HttpRoutes.TempImageController)]
public class TempImageController(
    ISender sender,
    ITempImageMapper tempImageMapper) : ApiController
{
    private readonly ISender _sender = sender;
    private readonly ITempImageMapper _mapper = tempImageMapper;

    [HttpPost]
    public async Task<ActionResult<CreateTempImagesResponse>> CreateTempImage(
        [FromBody] CreateTempImagesRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map(request);
        var result = await _sender.Send(command, cancellationToken);
        return result.IsError
            ? BadRequest(result.Errors)
            : Created(string.Empty, _mapper.Map(result.Value));
    }

    [HttpGet]
    public async Task<ActionResult<GetTempImageResponse>> GetTempImage(
        [FromQuery] GetTempImageRequest request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map(request);
        var result = await _sender.Send(query, cancellationToken);
        return result.IsError
            ? BadRequest(result.Errors)
            : Ok(_mapper.Map(result.Value));
    }
}