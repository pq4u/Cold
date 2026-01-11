using Cold.Packages.Core.DAL;
using Cold.Packages.Core.DAL.Repositories;
using Cold.Packages.Core.Services;
using Cold.Packages.Shared;
using Cold.Shared.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cold.Packages.Core;

public static class Extensions
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        services.AddPostgres<PackagesDbContext>();
        services.AddScoped<IPackagesModuleApi, PackagesModuleApi>();
        services.AddScoped<IPackageService, PackageService>();
        services.AddScoped<IPackageRentalService, PackageRentalService>();
        services.AddScoped<IPackageRepository, PackageRepository>();
        services.AddScoped<IPackageRentalRepository, PackageRentalRepository>();
        
        return services;
    }
}