using Cold.Packages.Core.Enums;

namespace Cold.Packages.Core.Entities;

public class PackageRental
{
    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    public PackageRentalStatus Status { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    
    public ICollection<PackageRentalItem> Items { get; set; } = new List<PackageRentalItem>();
}