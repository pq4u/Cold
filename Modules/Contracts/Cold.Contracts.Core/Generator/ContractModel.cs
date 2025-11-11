namespace Cold.Contracts.Core.Generator;

public class ContractModel
{
    public string Number { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Status { get; set; }
    public bool IsAccepted { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public DateTimeOffset? SignedDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public IReadOnlyList<string>? ProductsNames { get; set; }
}