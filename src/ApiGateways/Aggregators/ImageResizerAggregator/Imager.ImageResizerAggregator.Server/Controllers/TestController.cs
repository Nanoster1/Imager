using Dapr.Client;

using Imager.Dapr;

using Imager.ImageResizerAggregator.Server.Controllers.Common;
using Imager.ImageResizerService.Contracts.Events;
using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerAggregator.Server.Controllers;

[Route("/[controller]")]
public class TestController(DaprClient daprClient) : ApiController
{
    private readonly DaprClient _daprClient = daprClient;

    [HttpPost]
    public async Task<ActionResult> Test(IFormFile image)
    {
        const string userId = "123";
        using var stream = new MemoryStream();
        image.CopyTo(stream);
        var imgInBytes = stream.ToArray();

        var client = DaprClient.CreateInvokeHttpClient();

        var request = new CreateTempImagesRequest(userId, [imgInBytes]);
        var result = await (await client.PostAsJsonAsync("http://image-store-service/temp-images", request))
            .Content.ReadFromJsonAsync<CreateTempImagesResponse>();

        // var request2 = new GetTempImageRequest(result!.ImagesIds[0], userId);
        // var uri = new Uri($"http://image-store-service/temp-images?imageId={request2.ImageId}&userId={request2.UserId}");
        // var result2 = await client.GetAsync(uri);
        // result2.EnsureSuccessStatusCode();
        // var r2 = await result2.Content.ReadFromJsonAsync<GetTempImageResponse>();

        var @event = new ResizeImageEvent(result!.ImagesIds[0], userId, 100, 100, Path.GetExtension(image.FileName).Trim('.'));
        await _daprClient.PublishEventAsync(DaprComponentsNames.ImagerPubsub, nameof(ResizeImageEvent), @event);
        return Ok();
    }
}