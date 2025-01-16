using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Entities;

public class Adoption : IEntity<int>
{
    public int Id { get; set; }
    
    /// <summary>
    /// The Id of the adoption request associated with this adoption
    /// </summary>
    public int AdoptionRequestId { get; set; }

    /// <summary>
    /// The adoption request related to this adoption
    /// </summary>
    public AdoptionRequest AdoptionRequest { get; set; }
}