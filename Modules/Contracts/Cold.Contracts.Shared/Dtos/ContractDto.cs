namespace Cold.Contracts.Shared.Dtos;

public class ContractDto
{
    public Guid Id { get; set; }
    public string ContractNumber { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public short ContractStatusId { get; set; }
    public bool IsAccepted { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public DateTimeOffset? SignedDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public List<Guid> ProductIds { get; set; } = new();
}