using Cold.Contracts.Core.Entities;

namespace Cold.Contracts.Core.Contracts.Repositories;

internal interface IContractProductRepository
{
    Task<IEnumerable<ContractProduct>> GetByContractIdAsync(Guid contractId);
    Task AddAsync(ContractProduct contractProduct);
    Task DeleteByContractIdAsync(Guid contractId);
}