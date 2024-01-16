using System.Net.Http.Headers;

namespace Imager.Web.Client.Services;

public class AuthMessageHandler(TokenService tokenService) : DelegatingHandler
{
    private readonly TokenService _tokenService = tokenService;


    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetTokenAsync());
        return await base.SendAsync(request, cancellationToken);
    }
}