namespace Cold.Packages.Shared.Dtos;

public class PackageRentalDto
{
    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    
    public ICollection<PackageRentalItemDto> Items { get; set; } = new List<PackageRentalItemDto>();
}