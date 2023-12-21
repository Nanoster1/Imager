using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

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

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out StringValues value))
            return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));

        var authorizationHeader = (string?)value ?? string.Empty;
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
        }

        string token = authorizationHeader["bearer".Length..].Trim();

        if (string.IsNullOrEmpty(token))
        {
            return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var secToken = handler.ReadJwtToken(token);

            var claims = secToken.Claims;

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (Exception ex)
        {
            return Task.FromResult(AuthenticateResult.Fail(ex.Message));
        }
    }
}