using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Entities;

public class Breed : IEntity<int>
{
    public int Id { get; set; }
    /// <summary>
    /// The Id of the species to which this breed belongs
    /// </summary>
    public int SpeciesId { get; set; }

    /// <summary>
    /// The species to which this breed belongs
    /// </summary>
    public Species Species { get; set; }

    /// <summary>
    /// The name of the breed
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The pets of this breed
    /// </summary>
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}