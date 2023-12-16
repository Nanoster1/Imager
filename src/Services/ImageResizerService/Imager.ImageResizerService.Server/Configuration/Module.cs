using Imager.Extensions.Configuration.Defaults;

namespace Imager.ImageResizerService.Server.Configuration;

public static class Module
{
    public static IConfigurationBuilder AddServerConfiguration(this IConfigurationBuilder builder)
    {
        builder.AddYamlFile("appsettings.yaml", false, false);
        builder.AddYamlFile("appsettings.Development.yaml", true, false);
        builder.AddEnvironmentVariables(ConfigurationDefaults.EnvironmentPrefix);
        return builder;
    }
}