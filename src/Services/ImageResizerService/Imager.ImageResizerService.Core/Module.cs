using Microsoft.Extensions.DependencyInjection;

namespace Imager.ImageResizerService.Core;

public static class Module
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(Module).Assembly));
        return services;
    }
}