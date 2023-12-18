using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Contracts.Routes;
using Imager.ImageStoreService.Server.Controllers.Common;
using Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageStoreService.Server.Controllers;

[Route(HttpRoutes.TempImage)]
public class TempImageController(
    ISender sender,
    ITempImageMapper tempImageMapper) : ApiController
{
    private readonly ISender _sender = sender;
    private readonly ITempImageMapper mapper = tempImageMapper;

    [HttpPost]
    public async Task<ActionResult<CreateTempImagesResponse>> CreateTempImage(
        [FromBody] CreateTempImagesRequest request,
        CancellationToken cancellationToken)
    {
        var command = mapper.Map(request);
        var result = await _sender.Send(command, cancellationToken);
        return result.IsError
            ? BadRequest(result.Errors)
            : Created(string.Empty, mapper.Map(result.Value));
    }

    [HttpGet]
    public async Task<ActionResult<GetTempImageResponse>> GetTempImage(
        [FromQuery] GetTempImageRequest request,
        CancellationToken cancellationToken)
    {
        var query = mapper.Map(request);
        var result = await _sender.Send(query, cancellationToken);
        return result.IsError
            ? BadRequest(result.Errors)
            : Ok(mapper.Map(result.Value));
    }
}