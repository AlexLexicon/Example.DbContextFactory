using Example.DbContextFactory.After.Database;
using Example.DbContextFactory.After.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.DbContextFactory.After.Services;
public interface IOwnerService
{
    Task<IReadOnlyList<Pet>> GetPetsByIdAsync(Guid ownerId);
    Task<IReadOnlyList<Owner>> GetAllOwnersAsync();
}
public class OwnerService : IOwnerService
{
    private readonly IDbContextFactory<PetsDbContext> _dbContextFactory;
    private readonly ICatService _catService;
    private readonly IDogService _dogService;

    public OwnerService(
        IDbContextFactory<PetsDbContext> dbContextFactory,
        ICatService catService,
        IDogService dogService)
    {
        _dbContextFactory = dbContextFactory;
        _catService = catService;
        _dogService = dogService;
    }

    public async Task<IReadOnlyList<Pet>> GetPetsByIdAsync(Guid ownerId)
    {
        var getCatsByOwnerIdTask = _catService.GetCatsByOwnerIdAsync(ownerId);
        var getDogsByOwnerIdTask = _dogService.GetDogsByOwnerIdAsync(ownerId);

        IReadOnlyList<Cat> cats = await getCatsByOwnerIdTask;
        IReadOnlyList<Dog> dogs = await getDogsByOwnerIdTask;

        return cats
            .Cast<Pet>()
            .Concat(dogs.Cast<Pet>())
            .ToList();
    }

    public async Task<IReadOnlyList<Owner>> GetAllOwnersAsync()
    {
        var db = await _dbContextFactory.CreateDbContextAsync();

        return await db.Owners.ToListAsync();
    }
}
