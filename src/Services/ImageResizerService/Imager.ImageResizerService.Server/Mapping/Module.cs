using System.Reflection;

using Mapster;

namespace Imager.ImageResizerService.Server.Mapping;

public static class Module
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var mappers = typeof(Module).Assembly.GetTypes()
            .Select(t => new { Type = t, Interface = t.GetInterfaces().FirstOrDefault() })
            .Where(t => t.Interface is not null && t.Interface.GetCustomAttribute<MapperAttribute>() is not null);

        foreach (var mapper in mappers)
        {
            services.AddScoped(mapper.Interface!, mapper.Type);
        }

        return services;
    }
}