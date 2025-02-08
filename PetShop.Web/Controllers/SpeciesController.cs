using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Models.Species;

namespace PetShop.Controllers;

[Authorize(Roles = "Admin")]
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class SpeciesController : Controller
{
    private readonly ISpeciesService _speciesService;
    private readonly IMapper _mapper;

    public SpeciesController(ISpeciesService speciesService, IMapper mapper)
    {
        _speciesService = speciesService;
        _mapper = mapper;
    }

    [Authorize]
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

    public async Task<IActionResult> Details(int id)
    {
        var species = await _speciesService.GetSpeciesByIdAsync(id);
        return View(species);
    }

    public IActionResult CreateSpecies()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSpecies(Species species)
    {
        await _speciesService.CreateSpeciesAsync(species);
        return RedirectToAction(nameof(SpeciesDashboard));
    }

    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _speciesService.GetSpeciesByIdAsync(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        var speciesViewModel = _mapper.Map<SpeciesViewModel>(result.Value);
        return View(speciesViewModel);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SpeciesViewModel speciesViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(speciesViewModel);
        }

        var species = _mapper.Map<Species>(speciesViewModel);
        await _speciesService.UpdateSpeciesAsync(species);

        return RedirectToAction(nameof(SpeciesDashboard));
    }

    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var species = await _speciesService.GetSpeciesByIdAsync(id);
        return View(species.Value);
    }
    
    [Authorize]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _speciesService.DeleteSpeciesAsync(id);
        return RedirectToAction(nameof(SpeciesDashboard));
    }
}