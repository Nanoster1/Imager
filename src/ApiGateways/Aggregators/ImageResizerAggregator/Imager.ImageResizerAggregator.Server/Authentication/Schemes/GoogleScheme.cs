using Imager.ImageResizerAggregator.Server.Authentication.Validators;

using Microsoft.AspNetCore.Authentication;

namespace Imager.ImageResizerAggregator.Server.Authentication.Schemes;

public static class GoogleScheme
{
    public const string SchemeName = nameof(GoogleScheme);

    public static void AddGoogleScheme(this AuthenticationBuilder builder)
    {
        builder.AddJwtBearer(SchemeName, options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenHandlers.Clear();
            options.TokenHandlers.Add(new GoogleTokenHandler());
        });
    }
}