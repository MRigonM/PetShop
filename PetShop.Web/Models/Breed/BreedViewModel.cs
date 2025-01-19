using PetShop.Models.Species;

namespace PetShop.Models.Breed;

public class BreedViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SpeciesId { get; set; }
    public SpeciesViewModel Species { get; set; }
}