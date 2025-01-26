using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Application.Interfaces;
using PetShop.Application.Models;
using PetShop.Domain.Entities;
using PetShop.Domain.Helpers;
using PetShop.Extensions;
using PetShop.Models.Breed;
using PetShop.Models.City;
using PetShop.Models.Pet;

namespace PetShop.Controllers;

public class PetController : Controller
{
    private readonly IPetService _petService;
    private readonly IBreedService _breedService;
    private readonly ISpeciesService _speciesService;
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;
    private readonly IValidator<PetCreateViewModel> _validator;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public PetController(
        IPetService petService,
        IBreedService breedService,
        ISpeciesService speciesService,
        ICountryService countryService,
        ICityService cityService,
        IValidator<PetCreateViewModel> validator,
        IUserAccessor userAccessor,
        IMapper mapper)
    {
        _petService = petService;
        _breedService = breedService;
        _speciesService = speciesService;
        _countryService = countryService;
        _cityService = cityService;
        _validator = validator;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<IActionResult> Index(QueryParams queryParams)
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

    [HttpGet("pet/details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var countriesResult = await _countryService.GetAllCountriesAsync();
        var citiesResult = await _cityService.GetAllCitiesAsync();
        var countriesList = countriesResult.Value ?? new List<Country>();
        var citiesList = citiesResult.Value ?? new List<City>();

        var result = await _petService.GetPetByIdWithUserAdoptionsAsync(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        var petViewModel = _mapper.Map<PetViewModel>(result.Value);

        return View(petViewModel);
    }

    [Authorize]
    public async Task<IActionResult> Create()
    {
        var breedsResult = await _breedService.GetAllBreedsAsync();
        var speciesResult = await _speciesService.GetAllSpeciesAsync();
        var countriesResult = await _countryService.GetAllCountriesAsync();
        var citiesResult = await _cityService.GetAllCitiesAsync();

        var speciesList = speciesResult.Value ?? new List<Species>();
        var breedsList = breedsResult.Value ?? new List<Breed>();
        var countriesList = countriesResult.Value ?? new List<Country>();
        var citiesList = citiesResult.Value ?? new List<City>();

        var petCreateViewModel = new PetCreateViewModel
        {
            Species = new SelectList(speciesList, "Id", "Name"),
            Breeds = new SelectList(breedsList, "Id", "Name"),
            Countries = new SelectList(countriesList, "Id", "Name"),
            Cities = new SelectList(citiesList, "Id", "Name"),
            AllCities = citiesList.Select(x => new CityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            }).ToList(),
            AllBreeds = breedsList.Select(x => new BreedViewModel
            {
                Id = x.Id,
                Name = x.Name,
                SpeciesId = x.SpeciesId,
            }).ToList()
        };

        return View(petCreateViewModel);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PetCreateViewModel petCreateViewModel)
    {
        var validationResult = await _validator.ValidateAsync(petCreateViewModel);

        var breedsResult = await _breedService.GetAllBreedsAsync();
        var speciesResult = await _speciesService.GetAllSpeciesAsync();
        var countriesResult = await _countryService.GetAllCountriesAsync();
        var citiesResult = await _cityService.GetAllCitiesAsync();

        var speciesList = speciesResult.Value ?? new List<Species>();
        var breedsList = breedsResult.Value ?? new List<Breed>();
        var countriesList = countriesResult.Value ?? new List<Country>();
        var citiesList = citiesResult.Value ?? new List<City>();

        if (validationResult.IsValid is false)
        {
            petCreateViewModel.Species = new SelectList(speciesList, "Id", "Name");
            petCreateViewModel.Breeds = new SelectList(breedsList, "Id", "Name");
            petCreateViewModel.Countries = new SelectList(countriesList, "Id", "Name");
            petCreateViewModel.Cities = new SelectList(citiesList, "Id", "Name");
            petCreateViewModel.AllBreeds = breedsList.Select(x => new BreedViewModel
            {
                Id = x.Id,
                Name = x.Name,
                SpeciesId = x.SpeciesId,
            }).ToList();

            petCreateViewModel.AllCities = citiesList.Select(x => new CityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            }).ToList();

            validationResult.AddErrorsToModelState(ModelState);

            TempData["ErrorMessage"] = "Failed to create the pet! Please try again!";
            return View(petCreateViewModel);
        }

        var petCreateRequest = _mapper.Map<PetCreateRequest>(petCreateViewModel);
        var petCreateResult = await _petService.CreatePetAsync(petCreateRequest);

        if (petCreateResult.IsSuccess is false)
        {
            petCreateViewModel.Species = new SelectList(speciesList, "Id", "Name");
            petCreateViewModel.Breeds = new SelectList(breedsList, "Id", "Name");
            petCreateViewModel.Countries = new SelectList(countriesList, "Id", "Name");
            petCreateViewModel.Cities = new SelectList(citiesList, "Id", "Name");
            petCreateViewModel.AllBreeds = petCreateViewModel.AllBreeds = breedsList.Select(x => new BreedViewModel
            {
                Id = x.Id,
                Name = x.Name,
                SpeciesId = x.SpeciesId,
            }).ToList();

            petCreateViewModel.AllCities = citiesList.Select(x => new CityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            }).ToList();

            petCreateResult.AddErrorsToModelState(ModelState);
            return View(petCreateViewModel);
        }

        TempData["SuccessMessage"] = "Pet created successfully!";

        petCreateViewModel.Species = new SelectList(speciesList, "Id", "Name");
        petCreateViewModel.Breeds = new SelectList(breedsList, "Id", "Name");
        petCreateViewModel.Countries = new SelectList(countriesList, "Id", "Name");
        petCreateViewModel.Cities = new SelectList(citiesList, "Id", "Name");

        return RedirectToAction("Details", "Pet", new { id = petCreateResult.Value });
    }

    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _petService.GetPetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound();
        }

        var pet = result.Value;

        var breedsResult = await _breedService.GetAllBreedsAsync();
        var breedsList = breedsResult.Value ?? new List<Breed>();

        var petEdit = new PetEditViewModel
        {
            Id = pet.Id,
            Name = pet.Name,
            BreedId = pet.BreedId,
            AgeYears = pet.AgeYears,
            About = pet.About,
            Breeds = new SelectList(breedsList, "Id", "Name", pet.BreedId)
        };

        return View(petEdit);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Pet pet)
    {
        await _petService.UpdatePetAsync(pet);
        return RedirectToAction("Profile", "Users");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var petResult = await _petService.GetPetByIdAsync(id);
        if (!petResult.IsSuccess)
        {
            return NotFound();
        }

        return View(petResult.Value);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = _userAccessor.GetUserId();
        var result = await _petService.DeletePetAsync(id, userId);

        if (!result.IsSuccess)
        {
            result.AddErrorsToModelState(ModelState);
            return View();
        }

        return RedirectToAction("Profile", "Users");
    }
}