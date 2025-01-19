using PetShop.Models.Breed;

namespace PetShop.Models.Species;

public class SpeciesViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<BreedViewModel> Breeds { get; set; } = new List<BreedViewModel>();
}