namespace Cold.Contracts.Shared.Dtos;

public class ContractAmendmentDto
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public string AmendmentNumber { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Reason { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}