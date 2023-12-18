using Serilog;

namespace Imager.ImageResizerAggregator.Server.Logging;

public static class Module
{
    public static ILoggingBuilder UseServerLogging(this ILoggingBuilder builder, IConfiguration config)
    {
        builder.ClearProviders();
        builder.AddSerilog(config);
        return builder;
    }

    private static ILoggingBuilder AddSerilog(this ILoggingBuilder builder, IConfiguration config)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.AddSerilog(Log.Logger);
        return builder;
    }
}