using System.Text;
using Cold.Auth.Core.DAL;
using Cold.Auth.Core.Entities;
using Cold.Auth.Core.Services;
using Cold.Shared.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Cold.Auth.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<AuthDbContext>();
        
        services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AuthDbContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] 
                        ?? throw new InvalidOperationException("Jwt:Key not configured"))),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateIssuer = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateAudience = true,
                };
            });
        
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}