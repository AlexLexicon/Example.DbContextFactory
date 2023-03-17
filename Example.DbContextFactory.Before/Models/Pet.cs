namespace Example.DbContextFactory.Before.Models;
public abstract class Pet
{
    public required Guid Id { get; set; }
    public required Guid OwnerId { get; set; }
}
