using Cold.Packages.Shared.Dtos;

namespace Cold.Packages.Core.Services;

public interface IPackageService
{
    Task<PackageDto?> GetAsync(Guid id);
    Task<IEnumerable<PackageDto>> GetAllAsync();
    Task<Guid> CreateAsync(PackageDto dto);
    Task UpdateAsync(PackageDto dto);
    Task DeleteAsync(Guid id);
}