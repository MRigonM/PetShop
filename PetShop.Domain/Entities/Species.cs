using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Entities;

public class Species : IEntity<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
}