using PetShop.Application.Models.PetModels;
using PetShop.Domain.Entities;

namespace PetShop.Application.Models.BreedModels;

public class BreedRequest
{
    public int Id { get; set; }
    public int SpeciesId { get; set; }
    public Species Species { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<PetRequest> Pets { get; set; } = new List<PetRequest>();
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}