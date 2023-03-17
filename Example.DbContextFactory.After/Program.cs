using Example.DbContextFactory.After.Database;
using Example.DbContextFactory.After.Models;
using Example.DbContextFactory.After.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var configuration = new ConfigurationBuilder();
configuration.AddUserSecrets<Program>();



services.AddScoped<ICatService, CatService>();
services.AddScoped<IDogService, DogService>();
services.AddScoped<IOwnerService, OwnerService>();
services.AddScoped<ISeedService, SeedService>();

services.AddDbContextFactory<PetsDbContext>(options =>
{
    options.UseSqlServer(configuration.Build().GetConnectionString("db"));
});



var provider = services.BuildServiceProvider();

var ownerService = provider.GetRequiredService<IOwnerService>();

IReadOnlyList<Owner> owners = await ownerService.GetAllOwnersAsync();

Console.WriteLine($"Owners:");
foreach (var owner in owners)
{
    Console.WriteLine($"{owner.Name}:");

    IReadOnlyList<Pet> pets = await ownerService.GetPetsByIdAsync(owner.Id);

    foreach (var pet in pets)
    {
        if (pet is Dog dog)
        {
            Console.WriteLine($"    Dog: {dog.Name}");
        }
        else if (pet is Cat cat)
        {
            Console.WriteLine($"    Cat: {cat.Name}");
        }
    }
}