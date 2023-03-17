using Example.DbContextFactory.After.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.DbContextFactory.After.Database;
public class PetsDbContext : DbContext
{
    public PetsDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Cat> Cats { get; set; }
    public DbSet<Dog> Dogs { get; set; }
}
