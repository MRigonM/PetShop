using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Models.Location;

namespace PetShop.Controllers;

[Authorize(Roles = "Admin")]
public class CountryController : Controller
{
    private readonly ICountryService _countryService;
    private readonly IMapper _mapper;

    public CountryController(ICountryService countryService, IMapper mapper)
    {
        _countryService = countryService;
        _mapper = mapper;
    }
    [Authorize(Roles = "Admin")]
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

    public async Task<IActionResult> Details(int id)
    {
        var country = await _countryService.GetCountryByIdAsync(id);
        return View(country);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Country country)
    {
        await _countryService.CreateCountryAsync(country);
        return RedirectToAction(nameof(CountryDashboard));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _countryService.GetCountryByIdAsync(id);
        
        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        var countryViewModel = _mapper.Map<CountryViewModel>(result.Value);
        return View(countryViewModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CountryViewModel countryViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(countryViewModel);
        }

        var country = _mapper.Map<Country>(countryViewModel);
        await _countryService.UpdateCountryAsync(country);
        
        return RedirectToAction(nameof(CountryDashboard));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var country = await _countryService.GetCountryByIdAsync(id);
        return View(country.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _countryService.DeleteCountryAsync(id);
        return RedirectToAction(nameof(CountryDashboard));
    }
}