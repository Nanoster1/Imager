using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.Authentication;
using Imager.ImageResizerAggregator.Server.Configuration;
using Imager.ImageResizerAggregator.Server.Logging;
using Imager.ImageResizerAggregator.Server.SignalR;

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
    services.AddServerSettings(configuration);
    services.AddServerSignalR();
    services.AddServerAuthentication();
    services.AddControllers().AddDapr();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
    });
}

var app = builder.Build();
{
    app.UseCors();
    app.UseExceptionHandler(HttpRoutes.ExceptionHandler);
    app.UseCloudEvents();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapHubs();
    app.MapSubscribeHandler();
}

app.Run();
