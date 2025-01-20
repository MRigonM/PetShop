using PetShop.Domain.Common;
using PetShop.Domain.Entities;

namespace PetShop.Application.Interfaces;

public interface IBreedService
{
    Task<Result<bool>> CreateBreedAsync(Breed breed, CancellationToken cancellationToken);
    Task<Result<List<Breed>>> GetAllBreedsAsync(CancellationToken cancellationToken);
    Task<Result<Breed>> GetBreedByIdAsync(int breedId, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateBreedAsync(Breed breed, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteBreedAsync(int breedId, CancellationToken cancellationToken);
}