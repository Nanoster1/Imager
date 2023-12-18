using Imager.ImageResizerAggregator.Contracts.Routes;
using Imager.ImageResizerAggregator.Server.SignalR.Hubs;
using Imager.ImageResizerAggregator.Server.SignalR.Services;

using Microsoft.AspNetCore.SignalR;

namespace Imager.ImageResizerAggregator.Server.SignalR;

public static class Module
{
    public static IServiceCollection AddServerSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddSingleton<IUserIdProvider, UserIdProvider>();
        return services;
    }

    public static IEndpointRouteBuilder MapHubs(this IEndpointRouteBuilder builder)
    {
        builder.MapHub<ResizeImageHub>(HttpRoutes.ResizeImageHub);
        return builder;
    }
}