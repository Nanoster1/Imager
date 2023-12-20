using Dapr.Client;

using Imager.ImageResizerAggregator.Server.Services.Implementations;

using Imager.ImageResizerAggregator.Server.Services.Interfaces;

using Refit;

using Throw;

namespace Imager.ImageResizerAggregator.Server.Services;

public static class Module
{
    private const string TempImageStoreServiceKey = "TempImageStoreService";
    private const string ImageStoreServiceKey = "ImageStoreService";

    public static IServiceCollection AddServerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefit(configuration);
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }

    private static IServiceCollection AddRefit(this IServiceCollection services, IConfiguration configuration)
    {
        var tempImageStoreServiceAddress = configuration
            .GetConnectionString(TempImageStoreServiceKey)
            .ThrowIfNull();

        var imageStoreServiceAddress = configuration
            .GetConnectionString(ImageStoreServiceKey)
            .ThrowIfNull();

        services.AddScoped<InvocationHandler>();

        services.AddRefitClient<ITempImageService>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(tempImageStoreServiceAddress))
            .AddHttpMessageHandler<InvocationHandler>();

        services.AddRefitClient<IImageService>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(tempImageStoreServiceAddress))
            .AddHttpMessageHandler<InvocationHandler>();

        return services;
    }
}