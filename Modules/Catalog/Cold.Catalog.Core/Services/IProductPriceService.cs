using Cold.Catalog.Shared.Dtos;

namespace Cold.Catalog.Core.Services;

public interface IProductPriceService
{
    Task<IReadOnlyList<ProductPriceDto>> GetByProductIdAsync(Guid id);
    Task<IReadOnlyList<ProductPriceDto>> GetAllAsync();
    Task<decimal?> GetPriceAsync(Guid productId, string classType, DateTimeOffset date);
    Task AddAsync(ProductPriceDto dto);
    Task UpdateAsync(ProductPriceDto dto);
}