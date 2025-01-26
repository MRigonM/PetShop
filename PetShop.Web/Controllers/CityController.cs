using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Domain.Entities;

namespace PetShop.Controllers;

public class CityController : Controller
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    public async Task<IActionResult> Index()
    {
        var cities = await _cityService.GetAllCitiesAsync();
        return View(cities);
    }

    public async Task<IActionResult> Details(int id)
    {
        var city = await _cityService.GetCityByIdAsync(id);
        return View(city);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(City city)
    {
        await _cityService.CreateCityAsync(city);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var city = await _cityService.GetCityByIdAsync(id);
        return View(city);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(City city)
    {
        await _cityService.UpdateCityAsync(city);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var city = await _cityService.GetCityByIdAsync(id);
        return View(city);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _cityService.DeleteCityAsync(id);
        return RedirectToAction(nameof(Index));
    }
}