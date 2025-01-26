using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Domain.Entities;

namespace PetShop.Controllers;

[Microsoft.AspNetCore.Components.Route("[controller]")]
public class SpeciesController : Controller
{
    private readonly ISpeciesService _speciesService;

    public SpeciesController(ISpeciesService speciesService)
    {
        _speciesService = speciesService;
    }

    public async Task<IActionResult> Index()
    {
        var species = await _speciesService.GetAllSpeciesAsync();
        return View(species);
    }

    public async Task<IActionResult> Details(int id)
    {
        var species = await _speciesService.GetSpeciesByIdAsync(id);
        return View(species);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Species species)
    {
        await _speciesService.CreateSpeciesAsync(species);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var species = await _speciesService.GetSpeciesByIdAsync(id);
        return View(species);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Species species)
    {
        await _speciesService.UpdateSpeciesAsync(species);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var species = await _speciesService.GetSpeciesByIdAsync(id);
        return View(species);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _speciesService.DeleteSpeciesAsync(id);
        return RedirectToAction(nameof(Index));
    }
}