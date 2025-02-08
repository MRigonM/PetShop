using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Domain.Helpers;
using PetShop.Models.Breed;
using PetShop.Models.Location;
using PetShop.Models.Pet;

namespace PetShop.Controllers;

[Authorize(Roles = "Admin")]
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class DashboardController : Controller
{
    private readonly IPetService _petService;
    private readonly IBreedService _breedService;
    private readonly ICityService _cityService;
    private readonly IMapper _mapper;
    
    public DashboardController(
        IPetService petService,
        IBreedService breedService,
        ICityService cityService,
        IMapper mapper)
    {
        _petService = petService;
        _breedService = breedService;
        _cityService = cityService;
        _mapper = mapper;
    }
    
    public async Task<IActionResult> BreedDashboard()
    {
        var result = await _breedService.GetAllBreedsAsync();
        
        if (!result.IsSuccess)
        {
            return View("Error");
        }

        var breedViewModels = _mapper.Map<IEnumerable<BreedViewModel>>(result.Value);

        return View(breedViewModels);
    }
    
    public async Task<IActionResult> CityDashboard()
    {
        var result = await _cityService.GetAllCitiesAsync();
        
        if (!result.IsSuccess)
        {
            return View("Error");
        }

        var cityViewModels = _mapper.Map<IEnumerable<CityViewModel>>(result.Value);

        return View(cityViewModels);
    }
    
    public async Task<IActionResult> PetDashboard(QueryParams queryParams)
    {
        var result = await _petService.GetAvailablePetsWithDetailsAsync(queryParams);

        if (!result.IsSuccess)
        {
            return View("Error");
        }

        var petViewModels = new PetListViewModel
        {
            Pets = _mapper.Map<IEnumerable<PetViewModel>>(result.Value.Pets),
            TotalPages = result.Value.TotalPages,
        };

        return View(petViewModels);
    }
}