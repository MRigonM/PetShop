using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Entities;

public class Species : IEntity<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Breed> Breeds { get; set; } = new List<Breed>();
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}