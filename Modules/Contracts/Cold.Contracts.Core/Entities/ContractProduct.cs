namespace Cold.Contracts.Core.Entities;

internal sealed class ContractProduct
{
    public Guid ContractId { get; private set; }
    public Contract Contract { get; private set; }
    public Guid ProductId { get; private set; }

    public ContractProduct(Guid contractId, Guid productId)
    {
        ContractId = contractId;
        ProductId = productId;
    }

    private ContractProduct()
    {
    }
}