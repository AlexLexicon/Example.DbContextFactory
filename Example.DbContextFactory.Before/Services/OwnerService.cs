using Example.DbContextFactory.Before.Database;
using Example.DbContextFactory.Before.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.DbContextFactory.Before.Services;
public interface IOwnerService
{
    Task<IReadOnlyList<Pet>> GetPetsByIdAsync(Guid ownerId);
    Task<IReadOnlyList<Owner>> GetAllOwnersAsync();
}
public class OwnerService : IOwnerService
{
    private readonly PetsDbContext _petsDbContext;
    private readonly ICatService _catService;
    private readonly IDogService _dogService;

    public OwnerService(
        PetsDbContext petsDbContext,
        ICatService catService,
        IDogService dogService)
    {
        _petsDbContext = petsDbContext;
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
        return await _petsDbContext.Owners.ToListAsync();
    }
}
