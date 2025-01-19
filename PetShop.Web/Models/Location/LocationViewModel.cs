using PetShop.Models.Pet;

namespace PetShop.Models.Location;

public class LocationViewModel
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public CityViewModel City { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public List<PetViewModel> Pets { get; set; } = new List<PetViewModel>();
}