using Cold.Catalog.Core.Entities;

namespace Cold.Catalog.Core.Categories.Repositories;

internal interface ICategoryRepository
{
    Task<Category> GetByIdAsync(Guid categoryId);
    Task<Category> GetByNameAsync(string name);
    Task<IEnumerable<Category>> GetAllAsync();
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Category category);
}