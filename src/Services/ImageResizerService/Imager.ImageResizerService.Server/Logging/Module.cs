using Serilog;

namespace Imager.ImageResizerService.Server.Logging;

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
        var serilogLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.AddSerilog(serilogLogger);
        return builder;
    }
}