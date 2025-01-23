using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Entities;

public class City : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public ICollection<Location> Locations { get; set; } = new List<Location>();
}