using Example.DbContextFactory.After.Database;
using Example.DbContextFactory.After.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.DbContextFactory.After.Services;
public interface IDogService
{
    Task<IReadOnlyList<Dog>> GetDogsByOwnerIdAsync(Guid ownerId);
}
public class DogService : IDogService
{
    private readonly IDbContextFactory<PetsDbContext> _dbContextFactory;

    public DogService(IDbContextFactory<PetsDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<Dog>> GetDogsByOwnerIdAsync(Guid ownerId)
    {
        var db = await _dbContextFactory.CreateDbContextAsync();

        return await db.Dogs
            .Where(c => c.OwnerId == ownerId)
            .ToListAsync();
    }
}