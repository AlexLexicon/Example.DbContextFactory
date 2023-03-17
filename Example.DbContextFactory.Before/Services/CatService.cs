using Example.DbContextFactory.Before.Database;
using Example.DbContextFactory.Before.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.DbContextFactory.Before.Services;
public interface ICatService
{
    Task<IReadOnlyList<Cat>> GetCatsByOwnerIdAsync(Guid ownerId);
}
public class CatService : ICatService
{
    private readonly PetsDbContext _petsDbContext;

    public CatService(PetsDbContext petsDbContext)
    {
        _petsDbContext = petsDbContext;
    }

    public async Task<IReadOnlyList<Cat>> GetCatsByOwnerIdAsync(Guid ownerId)
    {
        return await _petsDbContext.Cats
            .Where(c => c.OwnerId == ownerId)
            .ToListAsync();
    }
}
