using System.Security.Claims;
using System.Text.Encodings.Web;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;

using Imager.ImageResizerAggregator.Server.Authentication.Constants;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Imager.ImageResizerAggregator.Server.Authentication.Handlers;

public class GoogleAuthenticationOptions : AuthenticationSchemeOptions
{
}

public class GoogleAuthenticationHandler : AuthenticationHandler<GoogleAuthenticationOptions>
{
    public GoogleAuthenticationHandler(IOptionsMonitor<GoogleAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out StringValues value))
            return AuthenticateResult.Fail("Unauthorized");

        var authorizationHeader = (string?)value ?? string.Empty;
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return AuthenticateResult.NoResult();
        }

        if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        string token = authorizationHeader["bearer".Length..].Trim();

        if (string.IsNullOrEmpty(token))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var credential = GoogleCredential.FromAccessToken(token);
        var service = new Oauth2Service(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential
        });

        try
        {
            var userInfo = await service.Userinfo.Get().ExecuteAsync();
            var userId = userInfo.Id;

            var claims = new List<Claim> { new(UserClaimTypes.Id, userId) };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex.Message);
        }
    }
}