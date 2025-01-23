using PetShop.Application.Models.BreedModels;
using PetShop.Application.Models.PetModels;

namespace PetShop.Application.Models.SpeciesModels;

public class SpeciesRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<BreedRequest> Breeds { get; set; } = new List<BreedRequest>();
    public ICollection<PetRequest> Pets { get; set; } = new List<PetRequest>();
}