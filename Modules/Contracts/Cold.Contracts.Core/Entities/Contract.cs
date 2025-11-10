namespace Cold.Contracts.Core.Entities;

internal sealed class Contract
{
    public Guid Id { get; private set; }
    public string ContractNumber { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public short ContractStatusId { get; private set; }
    public ContractStatus ContractStatus { get; private set; }
    public bool IsAccepted { get; private set; }
    public DateTimeOffset StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }
    public DateTimeOffset? SignedDate { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public ICollection<ContractProduct> ContractProducts { get; private set; }
    public ICollection<ContractAmendment> Amendments { get; private set; }

    public Contract(Guid id, string contractNumber, string title, string content, short contractStatusId,
                   bool isAccepted, DateTimeOffset startDate, DateTimeOffset? endDate,
                   DateTimeOffset? signedDate)
    {
        Id = id;
        ContractNumber = contractNumber;
        Title = title;
        Content = content;
        ContractStatusId = contractStatusId;
        IsAccepted = isAccepted;
        StartDate = startDate;
        EndDate = endDate;
        SignedDate = signedDate;
        CreatedAt = DateTimeOffset.UtcNow;
        ContractProducts = new HashSet<ContractProduct>();
        Amendments = new HashSet<ContractAmendment>();
    }

    private Contract()
    {
    }

    public void UpdateStatus(short contractStatusId, bool isAccepted, DateTimeOffset? signedDate)
    {
        ContractStatusId = contractStatusId;
        IsAccepted = isAccepted;
        SignedDate = signedDate;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}