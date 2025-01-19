namespace PetShop.Models.Location;

public class CountryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<CityViewModel> Cities { get; set; } = new List<CityViewModel>();
}