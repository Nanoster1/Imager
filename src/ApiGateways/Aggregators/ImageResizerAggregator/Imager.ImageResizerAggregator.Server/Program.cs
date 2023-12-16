using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.Configuration;
using Imager.ImageResizerAggregator.Server.Logging;

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
    builder.Services.AddServerSettings(configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDaprClient();
}

var app = builder.Build();
{
    app.UseExceptionHandler(HttpRoutes.ExceptionHandler);
    app.UseCloudEvents();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();
}

app.Run();
