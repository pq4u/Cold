using Cold.Catalog.Core.Categories.Repositories;
using Cold.Catalog.Core.DAL;
using Cold.Catalog.Core.DAL.Repositories;
using Cold.Catalog.Core.Services;
using Cold.Shared.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Cold.Catalog.Core;

public static class Extensions
{
    public static void AddCoreLayer(this IServiceCollection services)
    {
        services.AddPostgres<CatalogDbContext>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductPriceService, ProductPriceService>();
    }
}