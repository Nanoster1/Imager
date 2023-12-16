using Dapr.Client;

using Imager.ImageResizerAggregator.Server.Controllers.Common;
using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;

using Microsoft.AspNetCore.Mvc;

namespace Imager.ImageResizerAggregator.Server.Controllers;

[Route("/[controller]")]
public class TestController(DaprClient daprClient) : ApiController
{
    private readonly DaprClient _daprClient = daprClient;

    [HttpGet]
    public async Task<ActionResult> Test([FromServices] ILogger<TestController> logger)
    {
        const string userId = "123";
        var client = DaprClient.CreateInvokeHttpClient();

        var request = new CreateTempImagesRequest(userId, [[1, 2, 3]]);
        var result = await (await client.PostAsJsonAsync("http://image-store-service/temp-images", request))
            .Content.ReadFromJsonAsync<CreateTempImageResponse>();

        var request2 = new GetTempImageRequest(result!.ImagesIds[0], userId);
        var uri = new Uri($"http://image-store-service/temp-images?imageId={request2.ImageId}&userId={request2.UserId}");
        var result2 = await client.GetAsync(uri);
        result2.EnsureSuccessStatusCode();
        var r2 = await result2.Content.ReadFromJsonAsync<GetTempImageResponse>();
        return Ok(new { create = result, get = r2 });
    }
}