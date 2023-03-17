using Example.DbContextFactory.After.Database;
using Example.DbContextFactory.After.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.DbContextFactory.After.Services;
public interface ISeedService
{
    Task SeedDatabaseAsync();
}
public class SeedService : ISeedService
{
    private readonly IDbContextFactory<PetsDbContext> _dbContextFactory;

    public SeedService(IDbContextFactory<PetsDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task SeedDatabaseAsync()
    {
        var db = await _dbContextFactory.CreateDbContextAsync();

        var alex = new Owner
        {
            Id = Guid.NewGuid(),
            Name = "Alex",
        };
        await db.Owners.AddAsync(alex);
        await db.Cats.AddAsync(new Cat
        {
            Id = Guid.NewGuid(),
            OwnerId = alex.Id,
            Name = "Whiskers",
            Age = 5,
        });
        await db.Dogs.AddAsync(new Dog
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
        await db.Owners.AddAsync(bob);
        await db.Cats.AddAsync(new Cat
        {
            Id = Guid.NewGuid(),
            OwnerId = bob.Id,
            Name = "Oscar",
            Age = 11,
        });
        await db.Dogs.AddAsync(new Dog
        {
            Id = Guid.NewGuid(),
            OwnerId = bob.Id,
            Name = "Cooper",
            Age = 3,
        });
        await db.Dogs.AddAsync(new Dog
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
        await db.Owners.AddAsync(alice);
        await db.Cats.AddAsync(new Cat
        {
            Id = Guid.NewGuid(),
            OwnerId = alice.Id,
            Name = "Fluffy",
            Age = 6,
        });
        await db.Cats.AddAsync(new Cat
        {
            Id = Guid.NewGuid(),
            OwnerId = alice.Id,
            Name = "Lucky",
            Age = 9,
        });
        await db.Dogs.AddAsync(new Dog
        {
            Id = Guid.NewGuid(),
            OwnerId = alice.Id,
            Name = "Lucy",
            Age = 5,
        });
        await db.Dogs.AddAsync(new Dog
        {
            Id = Guid.NewGuid(),
            OwnerId = alice.Id,
            Name = "Rocky",
            Age = 1,
        });

        await db.SaveChangesAsync();
    }
}
