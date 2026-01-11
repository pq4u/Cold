using Cold.Packages.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Packages.Core.DAL.Repositories;

public class PackageRepository : IPackageRepository
{
    private readonly PackagesDbContext _context;

    public PackageRepository(PackagesDbContext context)
    {
        _context = context;
    }

    public async Task<Package?> GetAsync(Guid id)
        => await _context.Packages.SingleOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Package>> GetAllAsync()
        => await _context.Packages.ToListAsync();

    public async Task AddAsync(Package package)
    {
        await _context.Packages.AddAsync(package);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Package package)
    {
        _context.Packages.Update(package);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Package package)
    {
        _context.Packages.Remove(package);
        await _context.SaveChangesAsync();
    }
}