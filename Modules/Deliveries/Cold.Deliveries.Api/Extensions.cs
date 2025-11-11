using Cold.Deliveries.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cold.Deliveries.Api;

public static class Extensions
{
    public static IServiceCollection AddDeliveriesModule(this IServiceCollection services)
    {
        services.AddCoreLayer();

        return services;
    }

    public static IApplicationBuilder UseDeliveriesModule(this IApplicationBuilder app)
    {
        return app;
    }
}
