using PetShop.Domain.Enums;
using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Entities;

public class AdoptionRequest : IEntity<int>
{
    public int Id { get; set; }

    /// <summary>
    /// The status of the adoption request
    /// </summary>
    public AdoptionRequestStatus Status { get; set; }
    
    /// <summary>
    /// The Id of the pet associated with this adoption request
    /// </summary>
    public int? PetId { get; set; }

    /// <summary>
    /// The pet associated with this adoption request
    /// </summary>
    public Pet? Pet { get; set; }

    /// <summary>
    /// The adoption related to this request, if applicable
    /// </summary>
    public Adoption Adoption { get; set; }
    
    /// <summary>
    /// The ID of the location where the pet resides 
    /// </summary>
    public int LocationId { get; set; }

    /// <summary>
    /// The location entity associated with the adoption request.
    /// </summary>
    public Location Location { get; set; }

    /// <summary>
    /// The contact number provided by the requester.
    /// </summary>
    public string ContactNumber { get; set; }

    /// <summary>
    /// The email address of the requester.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Indicates whether the requester owns other pets (Yes/No).
    /// </summary>
    public bool OwnsOtherPets { get; set; }

    /// <summary>
    /// Details about other pets owned by the requester, including quantity, species, and breed.
    /// </summary>
    public string? OtherPetsDetails { get; set; }
}