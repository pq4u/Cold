using Cold.Catalog.Core.Categories.Repositories;
using Cold.Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Catalog.Core.DAL.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _dbContext;
    private readonly DbSet<Product> _products;

    public ProductRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
        _products = _dbContext.Products;
    }

    public async Task<Product> GetByIdAsync(Guid id)
        => await _products.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<Product> GetByNameAsync(string name)
        => await _products.SingleOrDefaultAsync(x => x.Name == name);

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _products.ToListAsync();
    
    public async Task AddAsync(Product product)
    {
        await _products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }
}