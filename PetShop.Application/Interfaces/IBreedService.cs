using PetShop.Domain.Common;
using PetShop.Domain.Entities;

namespace PetShop.Application.Interfaces;

public interface IBreedService
{
    Task<Result<bool>> CreateBreedAsync(Breed breed);
    Task<Result<List<Breed>>> GetAllBreedsAsync();
    Task<Result<Breed>> GetBreedByIdAsync(int breedId);
    Task<Result<bool>> UpdateBreedAsync(Breed breed);
    Task<Result<bool>> DeleteBreedAsync(int breedId);
}