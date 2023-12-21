using Dapr;
using Dapr.Client;

using Dumpify;

using Imager.Dapr;
using Imager.Dapr.Events;

using Imager.ImageResizerService.Contracts.Routes;
using Imager.ImageResizerService.Server.Controllers.Common;
using Imager.ImageResizerService.Server.Mapping.Mappers.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerService.Server.Controllers;

[Route(HttpRoutes.ResizeImageController)]
public class ResizeImageController(ISender sender, IResizeImageMapper mapper, DaprClient daprClient) : ApiController
{
    private readonly ISender _sender = sender;
    private readonly IResizeImageMapper _mapper = mapper;
    private readonly DaprClient _daprClient = daprClient;

    [HttpPost]
    [Topic(DaprComponentsNames.ImagerPubsub, nameof(OnResizeImageEvent))]
    public async Task ResizeImage(OnResizeImageEvent @event, [FromServices] ILogger<ResizeImageController> logger, CancellationToken cancellationToken)
    {
        var command = _mapper.Map(@event);
        logger.LogInformation(command.DumpText());
        var result = await _sender.Send(command, cancellationToken);
        if (result.IsError)
        {
            logger.LogInformation(result.DumpText());
            return;
        }
        else
        {
            var onImageResizedEvent = _mapper.Map(result.Value);
            logger.LogInformation(onImageResizedEvent.DumpText());
            await _daprClient.PublishEventAsync(
                DaprComponentsNames.ImagerPubsub,
                nameof(OnImageResizedEvent),
                onImageResizedEvent,
                cancellationToken);
        }
    }
}