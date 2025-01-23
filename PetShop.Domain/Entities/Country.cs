using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Entities;

public class Country : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<City> Cities { get; set; } = new List<City>();
}