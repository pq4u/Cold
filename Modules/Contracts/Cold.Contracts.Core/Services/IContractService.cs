using Cold.Contracts.Shared.Dtos;

namespace Cold.Contracts.Core.Services;

public interface IContractService
{
    Task<ContractDto> GetAsync(Guid contractId);
    Task<IReadOnlyList<ContractDto>> GetAllAsync();
    Task AddAsync(ContractDto dto);
    Task UpdateAsync(ContractDto dto);
}