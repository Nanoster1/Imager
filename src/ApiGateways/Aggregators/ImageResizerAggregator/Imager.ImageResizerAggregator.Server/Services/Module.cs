using Dapr.Client;

using Imager.ImageResizerAggregator.Server.Services.Interfaces;

using Refit;

using Throw;

namespace Imager.ImageResizerAggregator.Server.Services;

public static class Module
{
    private const string ImageStoreServiceKey = "ImageStoreService";

    public static IServiceCollection AddServerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefit(configuration);
        return services;
    }

    private static IServiceCollection AddRefit(this IServiceCollection services, IConfiguration configuration)
    {
        var imageStoreServiceAddress = configuration
            .GetConnectionString(ImageStoreServiceKey)
            .ThrowIfNull();

        services.AddScoped<InvocationHandler>();

        services.AddRefitClient<ITempImageService>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(imageStoreServiceAddress))
            .AddHttpMessageHandler<InvocationHandler>();

        return services;
    }
}