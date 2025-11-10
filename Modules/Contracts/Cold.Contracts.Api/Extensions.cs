using Cold.Contracts.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cold.Contracts.Api;

public static class Extensions
{
    public static IServiceCollection AddContractsModule(this IServiceCollection services)
    {
        services.AddCoreLayer();

        return services;
    }

    public static IApplicationBuilder UseContractsModule(this IApplicationBuilder app)
    {
        return app;
    }
}