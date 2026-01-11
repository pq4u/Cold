using Cold.Packages.Core.Entities;

namespace Cold.Packages.Core.DAL.Repositories;

public interface IPackageRepository
{
    Task<Package?> GetAsync(Guid id);
    Task<IEnumerable<Package>> GetAllAsync();
    Task AddAsync(Package package);
    Task UpdateAsync(Package package);
    Task DeleteAsync(Package package);
}