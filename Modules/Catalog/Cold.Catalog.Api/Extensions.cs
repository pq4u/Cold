using Cold.Catalog.Core;
using Cold.Catalog.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cold.Catalog.Api;

public static class Extensions
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services)
    {
        services.AddCoreLayer();
        
        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        return app;
    }
    
}