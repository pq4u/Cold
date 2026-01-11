using Cold.Packages.Core.DAL.Repositories;
using Cold.Packages.Core.Entities;
using Cold.Packages.Core.Enums;
using Cold.Packages.Core.Services;
using Cold.Packages.Shared.Dtos;
using Cold.Shared.Exceptions;

namespace Cold.Packages.Core.Services;

public class PackageRentalService : IPackageRentalService
{
    private readonly IPackageRentalRepository _rentalRepository;
    private readonly IPackageRepository _packageRepository;

    public PackageRentalService(IPackageRentalRepository rentalRepository, IPackageRepository packageRepository)
    {
        _rentalRepository = rentalRepository;
        _packageRepository = packageRepository;
    }

    public async Task<Guid> RequestRentalAsync(CreatePackageRentalRequestDto dto)
    {
        var rental = new PackageRental
        {
            Id = Guid.NewGuid(),
            SupplierId = dto.SupplierId,
            Status = PackageRentalStatus.Requested,
            RequestDate = DateTime.UtcNow,
            Items = new List<PackageRentalItem>()
        };

        foreach (var itemDto in dto.Items)
        {
            var package = await _packageRepository.GetAsync(itemDto.PackageId);
            if (package is null)
            {
                throw new ArgumentException($"Package with ID '{itemDto.PackageId}' not found.");
            }

            rental.Items.Add(new PackageRentalItem
            {
                Id = Guid.NewGuid(),
                PackageId = itemDto.PackageId,
                Quantity = itemDto.Quantity
            });
        }

        await _rentalRepository.AddAsync(rental);
        return rental.Id;
    }

    public async Task<PackageRentalDto?> GetAsync(Guid id)
    {
        var rental = await _rentalRepository.GetAsync(id);
        return rental is null ? null : ToDto(rental);
    }

    public async Task<IEnumerable<PackageRentalDto>> GetRequestedRentalsAsync()
    {
        var rentals = await _rentalRepository.GetAllAsync(r => r.Status == PackageRentalStatus.Requested);
        return rentals.Select(ToDto);
    }

    public async Task<IEnumerable<PackageRentalDto>> GetActiveRentalsAsync()
    {
        var rentals = await _rentalRepository.GetAllAsync(r => r.Status == PackageRentalStatus.Active);
        return rentals.Select(ToDto);
    }

    public async Task ApproveRentalAsync(Guid rentalId)
    {
        var rental = await _rentalRepository.GetAsync(rentalId);
        if (rental is null || rental.Status != PackageRentalStatus.Requested)
        {
            throw new ArgumentException($"Rental with ID '{rentalId}' not found or not in 'Requested' state.");
        }

        foreach (var item in rental.Items)
        {
            var package = await _packageRepository.GetAsync(item.PackageId);
            if (package is null)
            {
                throw new ArgumentException($"Package with ID '{item.PackageId}' not found.");
            }
            
            if (package.Quantity < item.Quantity)
            {
                throw new ArgumentException($"Not enough stock for package '{package.Name}'. Requested: {item.Quantity}, Available: {package.Quantity}.");
            }

            package.Quantity -= item.Quantity;
            await _packageRepository.UpdateAsync(package);
        }

        rental.Status = PackageRentalStatus.Approved;
        rental.ApprovalDate = DateTime.UtcNow;
        
        rental.Status = PackageRentalStatus.Active; 

        await _rentalRepository.UpdateAsync(rental);
    }

    public async Task RejectRentalAsync(Guid rentalId)
    {
        var rental = await _rentalRepository.GetAsync(rentalId);
        if (rental is null || rental.Status != PackageRentalStatus.Requested)
        {
            throw new ArgumentException($"Rental with ID '{rentalId}' not found or not in 'Requested' state.");
        }

        rental.Status = PackageRentalStatus.Rejected;
        await _rentalRepository.UpdateAsync(rental);
    }

    public async Task ReturnRentalAsync(Guid rentalId)
    {
        var rental = await _rentalRepository.GetAsync(rentalId);
        if (rental is null || rental.Status != PackageRentalStatus.Active)
        {
            throw new ArgumentException($"Rental with ID '{rentalId}' not found or not in 'Active' state.");
        }

        foreach (var item in rental.Items)
        {
            var package = await _packageRepository.GetAsync(item.PackageId);
            if (package is null)
            {
                throw new ArgumentException($"Package with ID '{item.PackageId}' not found.");
            }
            package.Quantity += item.Quantity;
            await _packageRepository.UpdateAsync(package);
        }
        
        rental.Status = PackageRentalStatus.Returned;
        rental.ReturnDate = DateTime.UtcNow;
        await _rentalRepository.UpdateAsync(rental);
    }
    
    private static PackageRentalDto ToDto(PackageRental rental)
        => new()
        {
            Id = rental.Id,
            SupplierId = rental.SupplierId,
            Status = rental.Status.ToString(),
            RequestDate = rental.RequestDate,
            ApprovalDate = rental.ApprovalDate,
            ReturnDate = rental.ReturnDate,
            Items = rental.Items.Select(item => new PackageRentalItemDto
            {
                Id = item.Id,
                PackageId = item.PackageId,
                PackageName = item.Package.Name,
                Quantity = item.Quantity
            }).ToList()
        };
}