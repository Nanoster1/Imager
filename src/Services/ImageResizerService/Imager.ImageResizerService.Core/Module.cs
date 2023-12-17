using Dapr.Client;

using Imager.ImageResizerService.Core.Common.Services.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Refit;

using Throw;

namespace Imager.ImageResizerService.Core;

public static class Module
{
    private const string ImageStoreServiceKey = "ImageStoreService";

    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR();
        services.AddRefit(configuration);
        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(Module).Assembly));
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