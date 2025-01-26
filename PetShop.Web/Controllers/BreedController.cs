using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Domain.Entities;

namespace PetShop.Controllers;

[Route("[controller]")]
    public class BreedController : Controller
    {
        private readonly IBreedService _breedService;
        public BreedController(IBreedService breedService)
        {
            _breedService = breedService;
        }

        public async Task<IActionResult> Index()
        {
            var breeds = await _breedService.GetAllBreedsAsync();
            return View(breeds);
        }

        public async Task<IActionResult> Details(int id)
        {
            var breed = await _breedService.GetBreedByIdAsync(id);
            return View(breed);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Breed breed)
        {
            await _breedService.CreateBreedAsync(breed);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var breed = await _breedService.GetBreedByIdAsync(id);
            return View(breed);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Breed breed)
        {
            await _breedService.UpdateBreedAsync(breed);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var breed = await _breedService.GetBreedByIdAsync(id);
            return View(breed);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _breedService.DeleteBreedAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }