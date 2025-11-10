using Cold.Contracts.Core.Contracts.Repositories;
using Cold.Contracts.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Contracts.Core.DAL.Repositories;

internal class ContractProductRepository : IContractProductRepository
{
    private readonly ContractsDbContext _dbContext;
    private readonly DbSet<ContractProduct> _contractProducts;

    public ContractProductRepository(ContractsDbContext dbContext)
    {
        _dbContext = dbContext;
        _contractProducts = _dbContext.ContractProducts;
    }

    public async Task<IEnumerable<ContractProduct>> GetByContractIdAsync(Guid contractId)
        => await _contractProducts
            .Where(x => x.ContractId == contractId)
            .ToListAsync();

    public async Task AddAsync(ContractProduct contractProduct)
    {
        await _contractProducts.AddAsync(contractProduct);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteByContractIdAsync(Guid contractId)
    {
        var contractProductsToDelete = await _contractProducts
            .Where(x => x.ContractId == contractId)
            .ToListAsync();

        _contractProducts.RemoveRange(contractProductsToDelete);
        await _dbContext.SaveChangesAsync();
    }
}