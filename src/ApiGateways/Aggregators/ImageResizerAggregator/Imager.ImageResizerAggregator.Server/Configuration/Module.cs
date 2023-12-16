using Imager.Extensions.Configuration.Defaults;
using Imager.ImageResizerAggregator.Server.Configuration.Settings;

namespace Imager.ImageResizerAggregator.Server.Configuration;

public static class Module
{
    public static IConfigurationBuilder AddServerConfiguration(this IConfigurationBuilder builder)
    {
        builder.AddYamlFile("appsettings.yaml", false, false);
        builder.AddYamlFile("appsettings.Development.yaml", true, false);
        builder.AddEnvironmentVariables(ConfigurationDefaults.EnvironmentPrefix);
        return builder;
    }

    public static IServiceCollection AddServerSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FilesSettings>(configuration.GetSection(FilesSettings.SectionName));
        return services;
    }
}