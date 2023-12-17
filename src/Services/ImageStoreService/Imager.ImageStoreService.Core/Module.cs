using Imager.ImageStoreService.Core.Common.Repositories.Implementations;
using Imager.ImageStoreService.Core.Common.Repositories.Interfaces;
using Imager.ImageStoreService.Core.Common.Services.Implementations;
using Imager.ImageStoreService.Core.Common.Services.Interfaces;
using Imager.ImageStoreService.Core.Common.Settings;
using Imager.ImageStoreService.Core.TempImages.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Imager.ImageStoreService.Core;

public static class Module
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR();
        services.AddSettings(configuration);
        services.AddServices();
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(Module).Assembly));
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITempImageObjectRepository, TempImageObjectRepository>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITempImageObjectKeyGenerator, TempImageObjectKeyGenerator>();
        return services;
    }

    private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TempImagesSettings>(configuration.GetSection(TempImagesSettings.SectionName));
        services.Configure<DaprSettings>(configuration.GetSection(DaprSettings.SectionName));
        return services;
    }
}