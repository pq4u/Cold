using Cold.Packages.Core.DAL.Repositories;
using Cold.Packages.Core.Entities;
using Cold.Packages.Core.Services;
using Cold.Packages.Shared.Dtos;
using Cold.Shared.Exceptions;

namespace Cold.Packages.Core.Services;

public class PackageService : IPackageService
{
    private readonly IPackageRepository _repository;

    public PackageService(IPackageRepository repository)
    {
        _repository = repository;
    }

    public async Task<PackageDto?> GetAsync(Guid id)
    {
        var package = await _repository.GetAsync(id);
        return package is null ? null : ToDto(package);
    }

    public async Task<IEnumerable<PackageDto>> GetAllAsync()
    {
        var packages = await _repository.GetAllAsync();
        return packages.Select(ToDto);
    }

    public async Task<Guid> CreateAsync(PackageDto dto)
    {
        var package = new Package
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Quantity = dto.Quantity
        };
        await _repository.AddAsync(package);
        return package.Id;
    }

    public async Task UpdateAsync(PackageDto dto)
    {
        var package = await _repository.GetAsync(dto.Id);
        if (package is null)
        {
            throw new ArgumentException($"Package with ID '{dto.Id}' not found.");
        }

        package.Name = dto.Name;
        package.Description = dto.Description;
        package.Quantity = dto.Quantity;

        await _repository.UpdateAsync(package);
    }

    public async Task DeleteAsync(Guid id)
    {
        var package = await _repository.GetAsync(id);
        if (package is null)
        {
            throw new ArgumentException($"Package with ID '{id}' not found.");
        }
        
        await _repository.DeleteAsync(package);
    }

    private static PackageDto ToDto(Package package)
        => new()
        {
            Id = package.Id,
            Name = package.Name,
            Description = package.Description,
            Quantity = package.Quantity
        };
}