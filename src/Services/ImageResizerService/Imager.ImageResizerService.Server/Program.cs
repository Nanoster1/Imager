using Imager.ImageResizerService.Contracts.Routes;
using Imager.ImageResizerService.Core;
using Imager.ImageResizerService.Server.Configuration;
using Imager.ImageResizerService.Server.Logging;
using Imager.ImageResizerService.Server.Mapping;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
{
    configuration.AddServerConfiguration();
}

var logging = builder.Logging;
{
    logging.UseServerLogging(configuration);
}

var services = builder.Services;
{
    services.AddCore(configuration);
    services.AddMapping();
    services.AddControllers().AddDapr();
    services.AddDaprClient();
}

var app = builder.Build();
{
    app.UseExceptionHandler(HttpRoutes.ExceptionHandler);
    app.UseCloudEvents();
    app.MapControllers();
    app.MapSubscribeHandler();
}

app.Run();
