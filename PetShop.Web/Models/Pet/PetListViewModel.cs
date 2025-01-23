namespace PetShop.Models.Pet;

public class PetListViewModel
{
    public IEnumerable<PetViewModel> Pets { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}