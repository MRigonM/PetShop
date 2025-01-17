using PetShop.Application.Models.PetImagesModels;
using PetShop.Domain.Entities;
using PetShop.Domain.Enums;

namespace PetShop.Application.Models.PetModels;

public class PetResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SpeciesId { get; set; }
    public Species Species { get; set; }
    public int BreedId { get; set; }
    public Breed Breed { get; set; }
    public string AgeYears { get; set; }
    public string About { get; set; }
    public decimal Price { get; set; }
    public PetStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<PetImageResponse> PetImages { get; set; } = new List<PetImageResponse>();
    public int TotalPages { get; set; }
}