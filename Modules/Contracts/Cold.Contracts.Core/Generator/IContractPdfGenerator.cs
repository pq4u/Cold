namespace Cold.Contracts.Core.Generator;

public interface IContractPdfGenerator
{
    Task<byte[]> GenerateAsync(Guid contractId);
}