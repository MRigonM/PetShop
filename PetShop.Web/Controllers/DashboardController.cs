using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Domain.Helpers;
using PetShop.Models.Breed;
using PetShop.Models.Location;
using PetShop.Models.Pet;
using PetShop.Models.Species;

namespace PetShop.Controllers;

public class DashboardController : Controller
{
    private readonly IPetService _petService;
    private readonly ISpeciesService _speciesService;
    private readonly IBreedService _breedService;
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;
    private readonly ILocationService _locationService;
    private readonly IMapper _mapper;
    
    public DashboardController(
        IPetService petService,
        ISpeciesService speciesService,
        IBreedService breedService,
        ICountryService countryService,
        ICityService cityService,
        ILocationService locationService,
        IMapper mapper)
    {
        _petService = petService;
        _speciesService = speciesService;
        _breedService = breedService;
        _countryService = countryService;
        _cityService = cityService;
        _locationService = locationService;
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
    
    public async Task<IActionResult> SpeciesDashboard()
    {
        var result = await _speciesService.GetAllSpeciesAsync();
        
        if (!result.IsSuccess)
        {
            return View("Error");
        }

        var specieViewModels = _mapper.Map<IEnumerable<SpeciesViewModel>>(result.Value);

        return View(specieViewModels);
    }
    
    public async Task<IActionResult> CountryDashboard()
    {
        var result = await _countryService.GetAllCountriesAsync();
        
        if (!result.IsSuccess)
        {
            return View("Error");
        }

        var countriesViewModels = _mapper.Map<IEnumerable<CountryViewModel>>(result.Value);

        return View(countriesViewModels);
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