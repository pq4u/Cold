using Cold.Catalog.Shared.Dtos;

namespace Cold.Catalog.Core.Services;

public interface IProductPriceService
{
    Task<IReadOnlyList<ProductPriceDto>> GetByProductIdAsync(Guid id);
    Task<IReadOnlyList<ProductPriceDto>> GetAllAsync();
    Task AddAsync(ProductPriceDto dto);
    Task UpdateAsync(ProductPriceDto dto);
}