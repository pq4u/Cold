namespace Cold.Contracts.Core.Entities;

internal sealed class ContractAmendment
{
    public Guid Id { get; private set; }
    public Guid ContractId { get; private set; }
    public Contract Contract { get; private set; }
    public string AmendmentNumber { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string Reason { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public ContractAmendment(Guid id, Guid contractId, string amendmentNumber, string title,
                            string content, string reason)
    {
        Id = id;
        ContractId = contractId;
        AmendmentNumber = amendmentNumber;
        Title = title;
        Content = content;
        Reason = reason;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private ContractAmendment()
    {
    }
}