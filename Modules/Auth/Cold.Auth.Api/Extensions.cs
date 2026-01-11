using System.Reflection;
using Cold.Auth.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cold.Auth.Api;

public static class Extensions
{
    public static IServiceCollection AddAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore(configuration);
        services.AddControllers().AddApplicationPart(Assembly.GetExecutingAssembly());
        return services;
    }
    
    public static IApplicationBuilder UseAuthModule(this IApplicationBuilder app)
    {
        return app;
    }
}