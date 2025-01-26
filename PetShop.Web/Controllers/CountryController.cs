using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Domain.Entities;

namespace PetShop.Controllers;

public class CountryController : Controller
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    public async Task<IActionResult> Index()
    {
        var countries = await _countryService.GetAllCountriesAsync();
        return View(countries);
    }

    public async Task<IActionResult> Details(int id)
    {
        var country = await _countryService.GetCountryByIdAsync(id);
        return View(country);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Country country)
    {
        await _countryService.CreateCountryAsync(country);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var country = await _countryService.GetCountryByIdAsync(id);
        return View(country);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Country country)
    {
        await _countryService.UpdateCountryAsync(country);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var country = await _countryService.GetCountryByIdAsync(id);
        return View(country);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _countryService.DeleteCountryAsync(id);
        return RedirectToAction(nameof(Index));
    }
}