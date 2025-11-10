using Cold.Contracts.Shared.Dtos;

namespace Cold.Contracts.Core.Services;

public interface IContractAmendmentService
{
    Task<ContractAmendmentDto> GetAsync(Guid amendmentId);
    Task<IReadOnlyList<ContractAmendmentDto>> GetByContractIdAsync(Guid contractId);
    Task<IReadOnlyList<ContractAmendmentDto>> GetAllAsync();
    Task AddAsync(ContractAmendmentDto dto);
}