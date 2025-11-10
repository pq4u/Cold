using Cold.Contracts.Core.Contracts.Repositories;
using Cold.Contracts.Core.Entities;
using Cold.Contracts.Shared.Dtos;

namespace Cold.Contracts.Core.Services;

internal class ContractAmendmentService : IContractAmendmentService
{
    private readonly IContractAmendmentRepository _amendmentRepository;
    private readonly IContractRepository _contractRepository;

    public ContractAmendmentService(IContractAmendmentRepository amendmentRepository,
                                   IContractRepository contractRepository)
    {
        _amendmentRepository = amendmentRepository;
        _contractRepository = contractRepository;
    }

    public async Task<ContractAmendmentDto> GetAsync(Guid amendmentId)
    {
        var amendment = await _amendmentRepository.GetByIdAsync(amendmentId);
        return amendment is null ? null : MapToDto(amendment);
    }

    public async Task<IReadOnlyList<ContractAmendmentDto>> GetByContractIdAsync(Guid contractId)
    {
        var amendments = await _amendmentRepository.GetByContractIdAsync(contractId);
        return amendments.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<ContractAmendmentDto>> GetAllAsync()
    {
        var amendments = await _amendmentRepository.GetAllAsync();
        return amendments.Select(MapToDto).ToList();
    }

    public async Task AddAsync(ContractAmendmentDto dto)
    {
        var contract = await _contractRepository.GetByIdAsync(dto.ContractId);
        if (contract is null)
        {
            throw new ArgumentException("Contract does not exist");
        }

        var amendment = new ContractAmendment(
            dto.Id,
            dto.ContractId,
            dto.AmendmentNumber,
            dto.Title,
            dto.Content,
            dto.Reason
        );

        await _amendmentRepository.AddAsync(amendment);
    }

    private static ContractAmendmentDto MapToDto(ContractAmendment amendment)
        => Map<ContractAmendmentDto>(amendment);

    private static T Map<T>(ContractAmendment amendment) where T : ContractAmendmentDto, new()
        => new()
        {
            Id = amendment.Id,
            ContractId = amendment.ContractId,
            AmendmentNumber = amendment.AmendmentNumber,
            Title = amendment.Title,
            Content = amendment.Content,
            Reason = amendment.Reason,
            CreatedAt = amendment.CreatedAt
        };
}