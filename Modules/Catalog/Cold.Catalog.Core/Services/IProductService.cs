using Cold.Catalog.Shared.Dtos;

namespace Cold.Catalog.Core.Services;

public interface IProductService
{
    Task<ProductDto> GetAsync(Guid categoryId);
    Task<IReadOnlyList<ProductDto>> GetAllAsync();
    Task<IReadOnlyList<string>> GetNamesAsync(List<Guid> productIds);
    Task AddAsync(ProductDto dto);
    Task UpdateAsync(ProductDto dto);
    Task RemoveAsync(Guid categoryId);
}