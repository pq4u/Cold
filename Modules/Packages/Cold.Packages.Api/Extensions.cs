using System.Reflection;
using Cold.Packages.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cold.Packages.Api;

public static class Extensions
{
    public static IServiceCollection AddPackagesModule(this IServiceCollection services)
    {
        services.AddCoreLayer();
        
        return services;
    }

    public static IApplicationBuilder UsePackagesModule(this IApplicationBuilder app)
    {
        return app;
    }
}