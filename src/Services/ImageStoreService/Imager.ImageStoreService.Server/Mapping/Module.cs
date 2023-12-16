using Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces.Common;

namespace Imager.ImageStoreService.Server.Mapping;

public static class Module
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        var mappers = typeof(Module).Assembly.GetTypes()
            .Where(t => typeof(IMapper).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(t => new { InterfaceType = t.GetInterfaces().First(), ImplementationType = t });

        foreach (var mapper in mappers)
        {
            services.AddScoped(mapper.InterfaceType, mapper.ImplementationType);
        }

        return services;
    }
}