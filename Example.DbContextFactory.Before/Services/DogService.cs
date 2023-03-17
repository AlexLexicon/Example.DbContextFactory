using Example.DbContextFactory.Before.Database;
using Example.DbContextFactory.Before.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.DbContextFactory.Before.Services;
public interface IDogService
{
    Task<IReadOnlyList<Dog>> GetDogsByOwnerIdAsync(Guid ownerId);
}
public class DogService : IDogService
{
    private readonly PetsDbContext _petsDbContext;

    public DogService(PetsDbContext petsDbContext)
    {
        _petsDbContext = petsDbContext;
    }

    public async Task<IReadOnlyList<Dog>> GetDogsByOwnerIdAsync(Guid ownerId)
    {
        return await _petsDbContext.Dogs
            .Where(c => c.OwnerId == ownerId)
            .ToListAsync();
    }
}