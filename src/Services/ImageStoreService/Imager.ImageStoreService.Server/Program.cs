using Imager.ImageResizerAggregator.Server.Configuration;
using Imager.ImageStoreService.Contracts.Routes;
using Imager.ImageStoreService.Core;
using Imager.ImageStoreService.Server.Logging;
using Imager.ImageStoreService.Server.Mapping;

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
    services.AddControllers();
}

var app = builder.Build();
{
    app.UseExceptionHandler(HttpRoutes.ExceptionHandler);
    app.UseCloudEvents();
    app.MapControllers();
}

app.Run();
