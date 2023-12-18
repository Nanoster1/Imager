using Imager.ImageResizerAggregator.Server.Authentication.Schemes;

namespace Imager.ImageResizerAggregator.Server.Authentication;

public static class Module
{
    public static IServiceCollection AddServerAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(GoogleScheme.SchemeName).AddGoogleScheme();
        return services;
    }
}