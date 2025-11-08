using Cold.Catalog.Core.Categories.Repositories;
using Cold.Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Catalog.Core.DAL.Repositories;

internal class CategoryRepository : ICategoryRepository
{
    private readonly CatalogDbContext _dbContext;
    private readonly DbSet<Category> _categories;

    public CategoryRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
        _categories = _dbContext.Categories;
    }

    public async Task<Category> GetByIdAsync(Guid id)
        => await _categories.SingleOrDefaultAsync(x => x.Id == id);
    
    public async Task<Category> GetByNameAsync(string name)
        => await _categories.SingleOrDefaultAsync(x => x.Name == name);
    
    public async Task<IEnumerable<Category>> GetAllAsync()
        => await _categories.ToListAsync();
    
    public async Task AddAsync(Category category)
    {
        await _categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _categories.Update(category);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Category category)
    {
        _categories.Remove(category);
        await _dbContext.SaveChangesAsync();
    }
}