using Example.DbContextFactory.Before.Database;
using Example.DbContextFactory.Before.Models;

namespace Example.DbContextFactory.Before.Services;
public interface ISeedService
{
    Task SeedDatabaseAsync();
}
public class SeedService : ISeedService
{
    private readonly PetsDbContext _petsDbContext;

    public SeedService(PetsDbContext petsDbContext)
    {
        _petsDbContext = petsDbContext;
    }

    public async Task SeedDatabaseAsync()
    {
        var alex = new Owner
        {
            Id = Guid.NewGuid(),
            Name = "Alex",
        };
        await _petsDbContext.Owners.AddAsync(alex);
        await _petsDbContext.Cats.AddAsync(new Cat
        {
            Id = Guid.NewGuid(),
            OwnerId = alex.Id,
            Name = "Whiskers",
            Age = 5,
        });
        await _petsDbContext.Dogs.AddAsync(new Dog
        {
            Id = Guid.NewGuid(),
            OwnerId = alex.Id,
            Name = "Max",
            Age = 7,
        });

        var bob = new Owner
        {
            Id = Guid.NewGuid(),
            Name = "Bob",
        };
        await _petsDbContext.Owners.AddAsync(bob);
        await _petsDbContext.Cats.AddAsync(new Cat
        {
            Id = Guid.NewGuid(),
            OwnerId = bob.Id,
            Name = "Oscar",
            Age = 11,
        });
        await _petsDbContext.Dogs.AddAsync(new Dog
        {
            Id = Guid.NewGuid(),
            OwnerId = bob.Id,
            Name = "Cooper",
            Age = 3,
        });
        await _petsDbContext.Dogs.AddAsync(new Dog
        {
            Id = Guid.NewGuid(),
            OwnerId = bob.Id,
            Name = "Milo",
            Age = 3,
        });

        var alice = new Owner
        {
            Id = Guid.NewGuid(),
            Name = "Alice",
        };
        await _petsDbContext.Owners.AddAsync(alice);
        await _petsDbContext.Cats.AddAsync(new Cat
        {
            Id = Guid.NewGuid(),
            OwnerId = alice.Id,
            Name = "Fluffy",
            Age = 6,
        });
        await _petsDbContext.Cats.AddAsync(new Cat
        {
            Id = Guid.NewGuid(),
            OwnerId = alice.Id,
            Name = "Lucky",
            Age = 9,
        });
        await _petsDbContext.Dogs.AddAsync(new Dog
        {
            Id = Guid.NewGuid(),
            OwnerId = alice.Id,
            Name = "Lucy",
            Age = 5,
        });
        await _petsDbContext.Dogs.AddAsync(new Dog
        {
            Id = Guid.NewGuid(),
            OwnerId = alice.Id,
            Name = "Rocky",
            Age = 1,
        });

        await _petsDbContext.SaveChangesAsync();
    }
}
