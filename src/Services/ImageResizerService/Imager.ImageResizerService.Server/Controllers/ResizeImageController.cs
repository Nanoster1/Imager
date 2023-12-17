using Dapr;

using Dumpify;

using Imager.Dapr;
using Imager.ImageResizerService.Contracts.Events;

using Imager.ImageResizerService.Contracts.Routes;
using Imager.ImageResizerService.Server.Controllers.Common;
using Imager.ImageResizerService.Server.Mapping.Mappers.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerService.Server.Controllers;

[Route(HttpRoutes.ResizeImage)]
public class ResizeImageController(ISender sender, IResizeImageMapper mapper) : ApiController
{
    private readonly ISender _sender = sender;
    private readonly IResizeImageMapper _mapper = mapper;

    [HttpPost]
    [Topic(DaprComponentsNames.ImagerPubsub, nameof(ResizeImageEvent))]
    public async Task ResizeImage(
        ResizeImageEvent @event,
        [FromServices] ILogger<ResizeImageController> logger,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map(@event);
        var result = await _sender.Send(command, cancellationToken);
        logger.LogInformation("Image resized: {@Result}", result.DumpText());
    }
}