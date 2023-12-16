using Imager.ImageStoreService.Core.Domains.TempImages.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Imager.ImageStoreService.Core;

public static class Module
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(Module).Assembly));
        services.Configure<TempImagesSettings>(configuration.GetSection(TempImagesSettings.SectionName));
        return services;
    }
}