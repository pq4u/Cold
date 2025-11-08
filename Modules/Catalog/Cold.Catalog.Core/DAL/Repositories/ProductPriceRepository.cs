using Cold.Catalog.Core.Categories.Repositories;
using Cold.Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Catalog.Core.DAL.Repositories;

internal class ProductPriceRepository : IProductPriceRepository
{
    private readonly CatalogDbContext _dbContext;
    private readonly DbSet<ProductPrice> _productPrices;

    public ProductPriceRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
        _productPrices = _dbContext.ProductPrices;
    }

    public async Task<ProductPrice> GetAsync(Guid productId)
        => await _productPrices.SingleOrDefaultAsync(x => x.ProductId == productId);
    
    public async Task<IEnumerable<ProductPrice>> GetAllAsync()
        => await _productPrices.ToListAsync();
    
    public async Task AddAsync(ProductPrice productPrice)
    {
        await _productPrices.AddAsync(productPrice);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductPrice productPrice)
    {
        _productPrices.Update(productPrice);
        await _dbContext.SaveChangesAsync();
    }
}