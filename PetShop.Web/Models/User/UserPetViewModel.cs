using PetShop.Domain.Enums;

namespace PetShop.Models;

public class UserPetViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SpeciesName { get; set; }
    public string BreedName { get; set; }
    public int AgeYears { get; set; }
    public int AgeMonths { get; set; }
    public string About { get; set; }
    public PetStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LocationName { get; set; }
    public string UserName { get; set; }
    public string PhotoUrl { get; set; }
}