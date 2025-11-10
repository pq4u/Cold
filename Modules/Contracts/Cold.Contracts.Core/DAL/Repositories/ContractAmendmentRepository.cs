using Cold.Contracts.Core.Contracts.Repositories;
using Cold.Contracts.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Contracts.Core.DAL.Repositories;

internal class ContractAmendmentRepository : IContractAmendmentRepository
{
    private readonly ContractsDbContext _dbContext;
    private readonly DbSet<ContractAmendment> _amendments;

    public ContractAmendmentRepository(ContractsDbContext dbContext)
    {
        _dbContext = dbContext;
        _amendments = _dbContext.ContractAmendments;
    }

    public async Task<ContractAmendment> GetByIdAsync(Guid amendmentId)
        => await _amendments.SingleOrDefaultAsync(x => x.Id == amendmentId);

    public async Task<IEnumerable<ContractAmendment>> GetByContractIdAsync(Guid contractId)
        => await _amendments
            .Where(x => x.ContractId == contractId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<ContractAmendment>> GetAllAsync()
        => await _amendments.ToListAsync();

    public async Task AddAsync(ContractAmendment amendment)
    {
        await _amendments.AddAsync(amendment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ContractAmendment amendment)
    {
        _amendments.Remove(amendment);
        await _dbContext.SaveChangesAsync();
    }
}