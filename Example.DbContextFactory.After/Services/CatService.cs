using Example.DbContextFactory.After.Database;
using Example.DbContextFactory.After.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.DbContextFactory.After.Services;
public interface ICatService
{
    Task<IReadOnlyList<Cat>> GetCatsByOwnerIdAsync(Guid ownerId);
}
public class CatService : ICatService
{
    private readonly IDbContextFactory<PetsDbContext> _dbContextFactory;

    public CatService(IDbContextFactory<PetsDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<Cat>> GetCatsByOwnerIdAsync(Guid ownerId)
    {
        var db = await _dbContextFactory.CreateDbContextAsync();

        return await db.Cats
            .Where(c => c.OwnerId == ownerId)
            .ToListAsync();
    }
}
