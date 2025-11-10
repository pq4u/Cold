using Cold.Contracts.Core.Contracts.Repositories;
using Cold.Contracts.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Contracts.Core.DAL.Repositories;

internal class ContractRepository : IContractRepository
{
    private readonly ContractsDbContext _dbContext;
    private readonly DbSet<Contract> _contracts;

    public ContractRepository(ContractsDbContext dbContext)
    {
        _dbContext = dbContext;
        _contracts = _dbContext.Contracts;
    }

    public async Task<Contract> GetByIdAsync(Guid contractId)
        => await _contracts
            .Include(c => c.ContractProducts)
            .SingleOrDefaultAsync(x => x.Id == contractId);

    public async Task<Contract> GetByContractNumberAsync(string contractNumber)
        => await _contracts
            .Include(c => c.ContractProducts)
            .SingleOrDefaultAsync(x => x.ContractNumber == contractNumber);

    public async Task<IEnumerable<Contract>> GetAllAsync()
        => await _contracts
            .Include(c => c.ContractProducts)
            .ToListAsync();

    public async Task AddAsync(Contract contract)
    {
        await _contracts.AddAsync(contract);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contract contract)
    {
        _contracts.Update(contract);
        await _dbContext.SaveChangesAsync();
    }
}