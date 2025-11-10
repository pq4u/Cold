using Cold.Contracts.Core.Contracts.Repositories;
using Cold.Contracts.Core.Entities;
using Cold.Contracts.Shared.Dtos;

namespace Cold.Contracts.Core.Services;

internal class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;
    private readonly IContractProductRepository _contractProductRepository;

    public ContractService(IContractRepository contractRepository, IContractProductRepository contractProductRepository)
    {
        _contractRepository = contractRepository;
        _contractProductRepository = contractProductRepository;
    }

    public async Task<ContractDto> GetAsync(Guid contractId)
    {
        var contract = await _contractRepository.GetByIdAsync(contractId);
        return contract is null ? null : MapToDto(contract);
    }

    public async Task<IReadOnlyList<ContractDto>> GetAllAsync()
    {
        var contracts = await _contractRepository.GetAllAsync();
        return contracts.Select(MapToDto).ToList();
    }

    public async Task AddAsync(ContractDto dto)
    {
        if (await _contractRepository.GetByContractNumberAsync(dto.ContractNumber) is not null)
        {
            throw new ArgumentException("Contract with this number already exists");
        }

        var contract = new Contract(
            dto.Id,
            dto.ContractNumber,
            dto.Title,
            dto.Content,
            dto.ContractStatusId,
            dto.IsAccepted,
            dto.StartDate,
            dto.EndDate,
            dto.SignedDate
        );

        await _contractRepository.AddAsync(contract);

        foreach (var productId in dto.ProductIds)
        {
            var contractProduct = new ContractProduct(dto.Id, productId);
            await _contractProductRepository.AddAsync(contractProduct);
        }
    }

    public async Task UpdateAsync(ContractDto dto)
    {
        var contract = await _contractRepository.GetByIdAsync(dto.Id);
        if (contract is null)
        {
            throw new ArgumentException("Contract does not exist");
        }

        contract.UpdateStatus(
            dto.ContractStatusId,
            dto.IsAccepted,
            dto.SignedDate
        );

        await _contractRepository.UpdateAsync(contract);
    }

    private static ContractDto MapToDto(Contract contract)
        => Map<ContractDto>(contract);

    private static T Map<T>(Contract contract) where T : ContractDto, new()
        => new()
        {
            Id = contract.Id,
            ContractNumber = contract.ContractNumber,
            Title = contract.Title,
            Content = contract.Content,
            ContractStatusId = contract.ContractStatusId,
            IsAccepted = contract.IsAccepted,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            SignedDate = contract.SignedDate,
            CreatedAt = contract.CreatedAt,
            UpdatedAt = contract.UpdatedAt,
            ProductIds = contract.ContractProducts?.Select(cp => cp.ProductId).ToList() ?? new List<Guid>()
        };
}