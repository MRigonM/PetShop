namespace PetShop.Models.Pet;

public class UserViewModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<PetViewModel> Pets { get; set; }
}