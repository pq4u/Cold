using Cold.Contracts.Core.Entities;

namespace Cold.Contracts.Core.Contracts.Repositories;

internal interface IContractRepository
{
    Task<Contract> GetByIdAsync(Guid contractId);
    Task<Contract> GetByContractNumberAsync(string contractNumber);
    Task<IEnumerable<Contract>> GetAllAsync();
    Task AddAsync(Contract contract);
    Task UpdateAsync(Contract contract);
}