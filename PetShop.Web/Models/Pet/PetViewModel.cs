using PetShop.Models.Breed;
using PetShop.Models.Location;
using PetShop.Models.PetImage;

namespace PetShop.Models.Pet;

public class PetViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string About { get; set; }
    public List<PetImageViewModel> PetImages { get; set; } = new List<PetImageViewModel>();
    public BreedViewModel Breed { get; set; }
    public LocationViewModel Location { get; set; }
    public string AgeYears { get; set; }
    public PetShop.Domain.Entities.Pet Pet { get; set; }
    public int TotalPages { get; set; }
}