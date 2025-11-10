using Cold.Contracts.Core.Entities;

namespace Cold.Contracts.Core.Contracts.Repositories;

internal interface IContractAmendmentRepository
{
    Task<ContractAmendment> GetByIdAsync(Guid amendmentId);
    Task<IEnumerable<ContractAmendment>> GetByContractIdAsync(Guid contractId);
    Task<IEnumerable<ContractAmendment>> GetAllAsync();
    Task AddAsync(ContractAmendment amendment);
}