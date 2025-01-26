using PetShop.Domain.Common;
using PetShop.Domain.Entities;

namespace PetShop.Application.Interfaces;

public interface ISpeciesService
{
    Task<Result<bool>> CreateSpeciesAsync(Species species, CancellationToken cancellationToken);
    Task<Result<List<Species>>> GetAllSpeciesAsync(CancellationToken cancellationToken);
    Task<Result<Species>> GetSpeciesByIdAsync(int speciesId, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateSpeciesAsync(Species species, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteSpeciesAsync(int speciesId, CancellationToken cancellationToken);
}