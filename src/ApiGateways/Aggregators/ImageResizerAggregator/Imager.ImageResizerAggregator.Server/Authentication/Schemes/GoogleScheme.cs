using Imager.ImageResizerAggregator.Server.Authentication.Handlers;

using Microsoft.AspNetCore.Authentication;

namespace Imager.ImageResizerAggregator.Server.Authentication.Schemes;

public static class GoogleScheme
{
    public const string SchemeName = nameof(GoogleScheme);

    public static void AddGoogleScheme(this AuthenticationBuilder builder)
    {
        builder.AddScheme<GoogleAuthenticationOptions, GoogleAuthenticationHandler>(SchemeName, null);
    }
}