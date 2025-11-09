using Cold.Catalog.Shared.Dtos;

namespace Cold.Catalog.Core.Services;

public interface ICategoryService
{
    Task<CategoryDto> GetAsync(Guid categoryId);
    Task<IReadOnlyList<CategoryDto>> GetAllAsync();
    Task AddAsync(CategoryDto dto);
    Task UpdateAsync(CategoryDto dto);
    Task RemoveAsync(Guid categoryId);
}