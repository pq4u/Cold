using Cold.Catalog.Core.Entities;

namespace Cold.Catalog.Core.Categories.Repositories;

internal interface IProductPriceRepository
{
    Task<ProductPrice> GetAsync(Guid productId);
    Task<IEnumerable<ProductPrice>> GetAllAsync();
    Task AddAsync(ProductPrice productPrice);
    Task UpdateAsync(ProductPrice productPrice);
}