using System.Text.Json;
using Cold.Shared.Database;
using Cold.Shared.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Cold.Shared;

public static class Extensions
{
    private const string ApiTitle = "Cold API";
    private const string ApiVersion = "v1";
    
    public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddErrorHandling();
        services.AddPostgres(configuration);
        services.AddControllers();
        services.AddSwaggerGen(s =>
        {
            s.EnableAnnotations();
            s.CustomSchemaIds(x => x.FullName);
            s.SwaggerDoc(ApiVersion, new()
            {
                Title = ApiTitle,
                Version = ApiVersion
            });
        });
        
        return services;
    }

    public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
    {
        app.UseErrorHandling();
        app.UseSwagger();
        app.UseReDoc(rd =>
        {
            rd.RoutePrefix = "docs";
            rd.SpecUrl($"/swagger/{ApiVersion}/swagger.json");
            rd.DocumentTitle = ApiTitle;
        });

        app.UseRouting();

        return app;
    }
}