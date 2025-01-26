using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Domain.Entities;

namespace PetShop.Controllers;

public class LocationController : Controller
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task<IActionResult> Index()
    {
        var locations = await _locationService.GetAllLocationsAsync();
        return View(locations);
    }

    public async Task<IActionResult> Details(int id)
    {
        var location = await _locationService.GetLocationByIdAsync(id);
        return View(location);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Location location)
    {
        await _locationService.CreateLocationAsync(location);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var location = await _locationService.GetLocationByIdAsync(id);
        return View(location);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Location location)
    {
        await _locationService.UpdateLocationAsync(location);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var location = await _locationService.GetLocationByIdAsync(id);
        return View(location);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _locationService.DeleteLocationAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
