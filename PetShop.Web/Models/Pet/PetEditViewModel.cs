using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Models.Breed;
using PetShop.Models.Location;

namespace PetShop.Models.Pet;

public class PetEditViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BreedId { get; set; }
    public int SpeciesId { get; set; }
    public string AgeYears { get; set; }
    public string About { get; set; }
    public decimal Price { get; set; }
    public int CityId { get; set; }
    public int CountryId { get; set; }
    public string Address { get; set; }
    public SelectList Breeds { get; set; }
    public SelectList Species { get; set; }
    public SelectList Countries { get; set; }
    public SelectList Cities { get; set; }
    public SelectList Locations { get; set; }
    public List<BreedViewModel> AllBreeds { get; set; }
    public List<CityViewModel> AllCities { get; set; }
    public IEnumerable<IFormFile> ImageFiles { get; set; }

}