using Microsoft.AspNetCore.Identity;
using PetShop.Domain.Entities;
using PetShop.Domain.Enums;

namespace PetShop.Application.Models.Pet;

public class UserPetResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BreedId { get; set; }
    public Breed Breed { get; set; }
    public string AgeYears { get; set; }
    public string About { get; set; }
    public PetStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<PetImage> PetImages { get; set; }
    public string PostedByUserId { get; set; }
    public ApplicationUser User { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}