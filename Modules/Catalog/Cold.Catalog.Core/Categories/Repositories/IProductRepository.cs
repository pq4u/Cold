using Cold.Catalog.Core.Entities;

namespace Cold.Catalog.Core.Categories.Repositories;

internal interface IProductRepository
{
    Task<Product> GetAsync(Guid productId);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
}