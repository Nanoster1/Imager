using Imager.ImageStoreService.Core.Common.Persistance.Repositories;
using Imager.ImageStoreService.Core.Common.Persistance.Services.Interfaces;
using Imager.ImageStoreService.Data.Persistance.Repositories;
using Imager.ImageStoreService.Data.Persistance.Services;
using Imager.ImageStoreService.Data.Persistance.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Imager.ImageStoreService.Data;

public static class Module
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITempImageObjectRepository, TempImageObjectRepository>();
        services.AddScoped<ITempImageObjectKeyGenerator, TempImageObjectKeyGenerator>();
        services.AddHttpClient<TempImageObjectRepository>();
        services.Configure<DaprSettings>(configuration.GetSection(DaprSettings.SectionName));
        return services;
    }
}