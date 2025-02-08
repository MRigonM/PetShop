using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Models.Breed;

namespace PetShop.Models.Species;

public class SpecieEditViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public SelectList Breeds { get; set; }
    public List<BreedViewModel> AllBreeds { get; set; }
}